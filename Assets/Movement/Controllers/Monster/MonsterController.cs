using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static Skill;

public class MonsterController : GenericController
{
    [Header("State Machine")]
    public StateMachine<MonsterController> stateMachine;

    [Header("Enemy States")]
    public EnemyPatrolState enemyPatrolState;
    public EnemyChaseState enemyChaseState;
    public EnemyRunState enemyRunState;
    public EnemyAttackState enemyAttackState;
    public EnemyEatState enemyEatState;
    public EnemyInvestigateState enemyInvestigateState;
    public EnemyCaptureState enemyCaptureState;

    [Header("Monster States")]
    public MonsterStandingState standingState;
    public MonsterFollowState followState;
    public MonsterSwapingState swapState;
    public MonsterAttackState attackState;

    [Header("Following")]
    public NavMeshAgent navMeshAgent;

    [Header("Player Input")]
    public InputManager inputManager;

    [Header("Carochito Battler")]
    public CarochitoBattler carochitoBattler;

    [Header("Target")]
    public Transform target;

    [Header("SetUp")]
    [HideInInspector] public bool setUp = false;

    [Header("Behavior Distances")]
    public float fleeDistance = 5f;
    public float followDistance = 10f;
    public float loseDistance = 15f;

    [Header("Wander Settings")]
    public float wanderRadius = 20f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;

    [Header("Movement Speeds")]
    public float wanderSpeed = 3.5f;
    public float followSpeed = 4.5f;
    public float fleeSpeed = 6f;
    public bool waiting;
    public float waitTimer;

    [Header("Vision")]
    public EnemyVision enemyVision;
    public Transform foodPosition;

    [Header("Attack")]
    public float attackRange = 6f;
    public float attackCooldown = 1.5f;
    public float battleLoseDistance = 15f;
    public float idealCombatDistance = 4f;
    public float minCombatDistance = 2f;
    public float maxCombatDistance = 6f;
    public float attackTimer;

    public void SetUp(CarochitoBattler battler)
    {
        carochitoBattler = battler;

        // Components
        controller = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        animator = GetComponentInChildren<Animator>();
        enemyVision = GetComponentInChildren<EnemyVision>();

        inputManager = FindFirstObjectByType<InputManager>();

        // State Machine
        stateMachine = new StateMachine<MonsterController>();

        followState = new MonsterFollowState(this, stateMachine);
        standingState = new MonsterStandingState(this, stateMachine);
        swapState = new MonsterSwapingState(this, stateMachine);
        attackState = new MonsterAttackState(this, stateMachine);

        enemyPatrolState = new EnemyPatrolState(this, stateMachine);
        enemyRunState = new EnemyRunState(this, stateMachine);
        enemyChaseState = new EnemyChaseState(this, stateMachine);
        enemyAttackState = new EnemyAttackState(this, stateMachine);
        enemyInvestigateState = new EnemyInvestigateState(this, stateMachine);
        enemyEatState = new EnemyEatState(this, stateMachine);
        enemyCaptureState = new EnemyCaptureState(this, stateMachine);

        if (carochitoBattler != null)
        {
            if (carochitoBattler._isEnemy == true)
            {
                enemyVision.gameObject.SetActive(true);
                stateMachine.Initialize(enemyPatrolState);
            }
            else
            {
                enemyVision.gameObject.SetActive(false);
                stateMachine.Initialize(followState);

                // Dashes
                currentDashCharges = maxDashCharges;
                StartCoroutine(RechargeDashes());
            }
        }

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

        for (int i = 0; i < carochitoBattler.Carochito.Skill.Count; i++)
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

    #region Eat
    [Header("Eat")]
    public float eatDistance = 1.5f;
    public float eatDuration = 3f;
    public float eatTimer;
    public void Eat(Transform snackPosition)
    {
        foodPosition = snackPosition;

        navMeshAgent.SetDestination(foodPosition.position);

        stateMachine.ChangeState(enemyEatState);
    }

    public void FinishEating()
    {
        BerlinerBall food = foodPosition.GetComponent<BerlinerBall>();

        foreach (Flavour falvour in carochitoBattler.Carochito.Base.FavouriteFlavour)
        {
            if (food.flavour == falvour)
            {
                carochitoBattler.Carochito.CurrentCatchRate += 30;

            }
        }

        foreach (Flavour falvour in carochitoBattler.Carochito.Base.NeutralFlavours)
        {
            if (food.flavour == falvour)
            {
                carochitoBattler.Carochito.CurrentCatchRate += 15;

            }
        }

        foreach (Flavour falvour in carochitoBattler.Carochito.Base.HateFlavours)
        {
            if (food.flavour == falvour)
            {
                carochitoBattler.Carochito.CurrentCatchRate -= 15;

            }
        }

        Destroy(food.gameObject);

        stateMachine.ChangeState(enemyPatrolState);
    }
    #endregion

    #region Hear
    [Header("Hear")]
    public Transform soundPosition;

    public void Hear(Transform playerPosition)
    {
        if (stateMachine.currentState == enemyPatrolState)
        {
            soundPosition = playerPosition;
            stateMachine.ChangeState(enemyInvestigateState);
        }
    }

    #endregion

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

    #region Attack

    public void RandomAttack()
    {
        int randomSkill = Random.Range(0, carochitoBattler.Carochito.Skill.Count + 1);
        Attack(randomSkill);
    }

    public void Attack(int index)
    {
        Skill selectedSkill = carochitoBattler.Carochito.Skill[index];

        if (index >= 0 && index < carochitoBattler.Carochito.Skill.Count)
        {
            if (selectedSkill.State == SkillState.Ready && selectedSkill != null)
            {
                selectedSkill.State = SkillState.StartUp;
                selectedSkill.StartUpTime = selectedSkill.Base.FullStartUpTime;

                // Animate
                switch (selectedSkill.Base.AttackType)
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
    }

    void HandleSkill(int index)
    {
        Skill selectedSkill = carochitoBattler.Carochito.Skill[index];

        switch (selectedSkill.State)
        {
            case SkillState.StartUp:
                if (selectedSkill.StartUpTime > 0)
                {
                    selectedSkill.StartUpTime -= Time.deltaTime;
                }
                else
                {
                    selectedSkill.Base.Activate(carochitoBattler);
                    selectedSkill.State = SkillState.Active;
                    selectedSkill.ActiveTime = selectedSkill.Base.FullActiveTime;
                }
                break;
            case SkillState.Active:

                if (selectedSkill.ActiveTime > 0)
                {
                    selectedSkill.ActiveTime -= Time.deltaTime;
                }
                else
                {
                    selectedSkill.State = SkillState.Cooldown;
                    selectedSkill.CooldownTime = selectedSkill.Base.FullCooldown;

                    if (carochitoBattler._isEnemy == false)
                    {
                        carochitoBattler._allyAbilities.transform.GetChild(index).GetComponent<IconAbility>().StartCooldown(selectedSkill);
                    }
                }
                break;

            case SkillState.Cooldown:

                if (selectedSkill.CooldownTime > 0)
                {
                    selectedSkill.CooldownTime -= Time.deltaTime;
                }
                else
                {
                    selectedSkill.State = SkillState.Ready;
                }
                break;
        }
    }

    #endregion
}

