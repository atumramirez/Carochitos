using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static Skill;

public class MonsterController : GenericController
{
    [Header("State Machine")]
    public StateMachine<MonsterController> stateMachine;

    [Header("States")]
    public MonsterStandingState standingState;
    public MonsterFollowState followState;
    public MonsterSwapingState swapState;

    public MonsterAttackState attackState;

    [Header("Player Input")]
    public InputManager inputManager;

    [Header("Following")]
    public Transform owner;
    public NavMeshAgent navMeshAgent;

    [Header("Carochito")]
    public Carochito carochito;
    public HealthBar healthBar;

    public CarochitoHandler handler;

    [Header("Dash")]
    public float dashSpeed= 10f;
    public float dashDuration = 0.15f;

    public int currentDashCharges;
    public float dashRechargeTime = 2f;
    public float dashDelay = 0.5f;
    public int maxDashCharges = 3;
    private bool canDash = true;
    //private bool isDashing;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        inputManager = FindFirstObjectByType<InputManager>();

        // Handler
        handler = GetComponent<CarochitoHandler>();
        
        // Nav Mesh Agent
        navMeshAgent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine<MonsterController>();

        // Grounded
        followState = new MonsterFollowState(this, stateMachine);
        standingState = new MonsterStandingState(this, stateMachine);
        swapState = new MonsterSwapingState(this, stateMachine);

        attackState = new MonsterAttackState(this, stateMachine);

        stateMachine.Initialize(followState);

        // Skills
        AssignSkillsToSlots();

        cameraTransform = Camera.main.transform;

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;

        currentDashCharges = maxDashCharges;

        StartCoroutine(RechargeDashes());
    }

    private void Update()
    {
        stateMachine.currentState.LogicUpdate();

        for (int i = 0; i < skills.Count; i++)
        {
            HandleSkill(i);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && currentDashCharges > 0)
        {
            StartCoroutine(Dash());
        }
    }

    public IEnumerator Dash()
    {
        canDash = false;
        //isDashing = true;

        currentDashCharges--;

        float ogSpeed = playerSpeed;
        playerSpeed *= dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        playerSpeed = ogSpeed;
        //isDashing = false;

        yield return new WaitForSeconds(dashDelay);

        canDash = true;
    }

    private IEnumerator RechargeDashes()
    {
        while (true)
        {
            yield return new WaitForSeconds(dashRechargeTime);

            if (currentDashCharges < maxDashCharges)
            {
                currentDashCharges++;
            }
        }
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void GetOwner(Transform transform)
    {
        owner = transform;
    }

    [Header("Skills")]
    public List<SkillSlot> skills = new(4);

    [System.Serializable]
    public class SkillSlot
    {
        public Skill skill;
        public Cooldown cooldownUI;

        [HideInInspector] public float cooldownTime;
        [HideInInspector] public float activeTime;
        [HideInInspector] public SkillState state = SkillState.Ready;
    }

    void AssignSkillsToSlots()
    {
        skills.Clear();

        for (int i = 0; i < carochito.Skill.Count && i < 4; i++)
        {
            SkillSlot newSlot = new()
            {
                skill = carochito.Skill[i]
            };

            skills.Add(newSlot);
        }
    }

    public void Attack(int index)
    {
        SkillSlot slot = skills[index];

        if (slot.state == SkillState.Ready && slot.skill != null)
        {
            stateMachine.ChangeState(attackState);

            slot.skill.Base.Activate(gameObject);
            slot.state = SkillState.Active;
            slot.activeTime = slot.skill.Base.ActiveTime;
        }
    }

    void HandleSkill(int index)
    {
        SkillSlot slot = skills[index];

        switch (slot.state)
        {
            case SkillState.Active:
                if (slot.activeTime > 0)
                {
                    slot.activeTime -= Time.deltaTime;
                }
                else
                {
                    slot.state = SkillState.Cooldown;
                    slot.cooldownTime = slot.skill.Base.Cooldown;

                    stateMachine.ChangeState(standingState);

                    if (slot.cooldownUI != null)
                    {
                        slot.cooldownUI.StartCooldown();
                    }    
                }
                break;

            case SkillState.Cooldown:
                if (slot.cooldownTime > 0)
                {
                    slot.cooldownTime -= Time.deltaTime;
                }
                else
                {
                    slot.state = SkillState.Ready;
                }
                break;
        }
    }
}

