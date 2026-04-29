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

    [Header("Player Inputs")]
    public InputAction move;
    public InputAction attack;
    public InputAction swap;

    public InputAction ability1;
    public InputAction ability2;
    public InputAction ability3;
    public InputAction ability4;

    [Header("Following")]
    public Transform owner;
    public NavMeshAgent navMeshAgent;

    [Header("Carochito")]
    public Carochito carochito;
    public HealthBar healthBar;

    public CarochitoHandler handler;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

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

        // PLayer Input 
        playerInput = GetComponent<PlayerInput>();

        move = playerInput.actions.FindActionMap("Monster").FindAction("Move");
        swap = playerInput.actions.FindActionMap("Monster").FindAction("Switch");

        // Attacks s
        ability1 = playerInput.actions.FindActionMap("Monster").FindAction("Ability1");
        ability2 = playerInput.actions.FindActionMap("Monster").FindAction("Ability2");
        ability3 = playerInput.actions.FindActionMap("Monster").FindAction("Ability3");
        ability4 = playerInput.actions.FindActionMap("Monster").FindAction("Ability4");

        // Skills
        AssignSkillsToSlots();

        cameraTransform = Camera.main.transform;

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;
    }

    private void Update()
    {
        stateMachine.currentState.LogicUpdate();

        for (int i = 0; i < skills.Count; i++)
        {
            HandleSkill(i);
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
        public SkillBase skill;
        public Cooldown cooldownUI;

        [HideInInspector] public float cooldownTime;
        [HideInInspector] public float activeTime;
        [HideInInspector] public SkillState state = SkillState.Ready;
    }

    void AssignSkillsToSlots()
    {
        skills.Clear();

        for (int i = 0; i < carochito.Skills.Count && i < 4; i++)
        {
            SkillSlot newSlot = new();
            newSlot.skill = carochito.Skills[i];

            skills.Add(newSlot);
        }
    }

    public void Attack(int index)
    {
        SkillSlot slot = skills[index];

        if (slot.state == SkillState.Ready && slot.skill != null)
        {
            stateMachine.ChangeState(attackState);

            slot.skill.Activate(gameObject);
            slot.state = SkillState.Active;
            slot.activeTime = slot.skill.ActiveTime;
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
                    slot.cooldownTime = slot.skill.Cooldown;

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

