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
    public NavMeshAgent navMeshAgent;

    [Header("Carochito Battler")]
    public CarochitoTeamBattler carochitoTeamBattler;

    private bool setUp = false;

    public void SetUp(Transform owner)
    {
        // Components
        controller = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        carochitoTeamBattler = GetComponent<CarochitoTeamBattler>();
        animator = GetComponentInChildren<Animator>();
        inputManager = FindFirstObjectByType<InputManager>();

        // State Machine
        stateMachine = new StateMachine<MonsterController>();
        followState = new MonsterFollowState(this, stateMachine);
        standingState = new MonsterStandingState(this, stateMachine);
        swapState = new MonsterSwapingState(this, stateMachine);
        attackState = new MonsterAttackState(this, stateMachine);

        stateMachine.Initialize(followState);

        // Dashes
        currentDashCharges = maxDashCharges;
        StartCoroutine(RechargeDashes());

        // Camera
        cameraTransform = Camera.main.transform;

        normalColliderHeight = controller.height;
        gravityValue *= gravityMultiplier;

        setUp = true;
    }

    private void Update()
    {
        if (setUp == false)
            return;

        stateMachine.currentState.LogicUpdate();

        for (int i = 0; i < carochitoTeamBattler.Carochito.Skill.Count; i++)
        {
            HandleSkill(i);
        }
    }

    private void FixedUpdate()
    {
        if (setUp == false)
            return;

        stateMachine.currentState.PhysicsUpdate();
    }

    #region Dash
    [Header("Dash")]
    public float dashSpeed = 10f;
    public float dashDuration = 0.15f;

    public int currentDashCharges;
    public float dashRechargeTime = 2f;
    public float dashDelay = 0.5f;
    public int maxDashCharges = 3;
    private bool canDash = true;

    public void Dash()
    {
        if (canDash && currentDashCharges > 0)
        {
            StartCoroutine(StartDash());
        }
    }

    public IEnumerator StartDash()
    {

        Debug.Log("Dash");

        canDash = false;

        currentDashCharges--;

        float ogSpeed = playerSpeed;
        playerSpeed *= dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        playerSpeed = ogSpeed;

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
    #endregion

    public void Attack(int index)
    {
        if (carochitoTeamBattler.Carochito.Skill[index].State == SkillState.Ready && carochitoTeamBattler.Carochito.Skill[index] != null)
        {
            carochitoTeamBattler.Carochito.Skill[index].State = SkillState.StartUp;
            carochitoTeamBattler.Carochito.Skill[index].StartUpTime = carochitoTeamBattler.Carochito.Skill[index].Base.FullStartUpTime;

            // Animate
            switch (carochitoTeamBattler.Carochito.Skill[index].Base.AttackType)
            {
                case AttackType.Physical:
                    animator.SetTrigger("attack");
                    break;
                case AttackType.Special:
                    animator.SetTrigger("range");
                    break;
            }
        }
    }

    void HandleSkill(int index)
    {
        switch (carochitoTeamBattler.Carochito.Skill[index].State)
        {
            case SkillState.StartUp:
                if (carochitoTeamBattler.Carochito.Skill[index].StartUpTime > 0)
                {
                    carochitoTeamBattler.Carochito.Skill[index].StartUpTime -= Time.deltaTime;
                }
                else
                {
                    carochitoTeamBattler.Carochito.Skill[index].Base.Activate(carochitoTeamBattler);
                    carochitoTeamBattler.Carochito.Skill[index].State = SkillState.Active;
                    carochitoTeamBattler.Carochito.Skill[index].ActiveTime = carochitoTeamBattler.Carochito.Skill[index].Base.FullActiveTime;
                }
                break;
            case SkillState.Active:

                if (carochitoTeamBattler.Carochito.Skill[index].ActiveTime > 0)
                {
                    carochitoTeamBattler.Carochito.Skill[index].ActiveTime -= Time.deltaTime;
                }
                else
                {
                    carochitoTeamBattler.Carochito.Skill[index].State = SkillState.Cooldown;
                    carochitoTeamBattler.Carochito.Skill[index].CooldownTime = carochitoTeamBattler.Carochito.Skill[index].Base.FullCooldown;

                    carochitoTeamBattler.ability.transform.GetChild(index).GetComponent<IconAbility>().StartCooldown(carochitoTeamBattler.Carochito.Skill[index]);
                }
                break;

            case SkillState.Cooldown:

                if (carochitoTeamBattler.Carochito.Skill[index].CooldownTime > 0)
                {
                    carochitoTeamBattler.Carochito.Skill[index].CooldownTime -= Time.deltaTime;
                }
                else
                {
                    carochitoTeamBattler.Carochito.Skill[index].State = SkillState.Ready;
                }
                break;
        }
    }
}

