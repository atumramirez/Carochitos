using UnityEngine;
using UnityEngine.AI;

public class EnemyController: GenericController
{
    [Header("State Machine")]
    public StateMachine<EnemyController> stateMachine;

    [Header("States")]
    public EnemyPatrolState patrolState;
    public EnemyChaseState chaseState;
    public EnemyRunState runState;
    public EnemyAttackState attackState;
    public EnemyInvestigateState investigateState;

    [Header("Following")]
    public NavMeshAgent navMeshAgent;

    [Header("Carochito Battler")]
    public CarochitoEnemyBattler carochitoEnemyBattler;

    [Header("Target")]
    public Transform target;

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

    public bool isFollowing;
    public bool waiting;
    public float waitTimer;

    private void Start()
    {
        // Components
        navMeshAgent = GetComponent<NavMeshAgent>();
        carochitoEnemyBattler = GetComponent<CarochitoEnemyBattler>();

        // Maquina de Estados
        stateMachine = new StateMachine<EnemyController>();

        patrolState = new EnemyPatrolState(this, stateMachine);
        runState = new EnemyRunState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        investigateState = new EnemyInvestigateState(this, stateMachine);

        stateMachine.Initialize(patrolState);
    }

    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }
}

public class EnemyPatrolState : State<EnemyController>
{
    public EnemyPatrolState(EnemyController enemy, StateMachine<EnemyController> sm) : base(enemy, sm)
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void Enter()
    {
        SetNewDestination();
    }

    public override void LogicUpdate()
    {
        if (character.waiting)
        {
            character.waitTimer -= Time.deltaTime;

            if (character.waitTimer <= 0)
            {
                character.waiting = false;
                SetNewDestination();
            }

            return;
        }

        if (!character.navMeshAgent.pathPending && character.navMeshAgent.remainingDistance <= character.navMeshAgent.stoppingDistance)
        {
            character.waiting = true;
            character.waitTimer = Random.Range(character.minWaitTime, character.maxWaitTime);
        }
    }

    private void SetNewDestination()
    {
        Vector3 randomPoint = Random.insideUnitSphere * character.wanderRadius;
        randomPoint += character.transform.position;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, character.wanderRadius, NavMesh.AllAreas))
        {
            character.navMeshAgent.SetDestination(hit.position);
        }
    }
}

public class EnemyChaseState : State<EnemyController>
{
    public EnemyChaseState(EnemyController enemy, StateMachine<EnemyController> sm) : base(enemy, sm)
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void LogicUpdate()
    {
        if (character.target != null)
        {
            float distanceToTarget = Vector3.Distance(character.transform.position, character.target.position);

            if (distanceToTarget >= character.loseDistance)
            {
                stateMachine.ChangeState(character.patrolState);
            }
            
            character.navMeshAgent.SetDestination(character.target.position);
        }
    }
}

public class EnemyRunState : State<EnemyController>
{

    public EnemyRunState(EnemyController enemy, StateMachine<EnemyController> sm) : base(enemy, sm)
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void LogicUpdate()
    {
        if (character.target != null)
        {
            float distanceToTarget = Vector3.Distance(character.transform.position, character.target.position);

            if (distanceToTarget <= character.fleeDistance)
            {
                character.navMeshAgent.speed = character.fleeSpeed;
                FleeFromTarget();
            }
        }
    }

    private void FleeFromTarget()
    {
        Vector3 directionAway = (character.transform.position - character.target.position).normalized;
        Vector3 fleePosition = character.transform.position + directionAway * character.wanderRadius;

        if (NavMesh.SamplePosition(fleePosition, out NavMeshHit hit, character.wanderRadius, NavMesh.AllAreas))
        {
            character.navMeshAgent.SetDestination(hit.position);
        }
    }
}


public class EnemyAttackState : State<EnemyController>
{
    public EnemyAttackState(EnemyController enemy, StateMachine<EnemyController> sm) : base(enemy, sm)
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void Enter()
    {
   
    }

    public override void LogicUpdate()
    {

    }
}

public class EnemyInvestigateState : State<EnemyController>
{

    public EnemyInvestigateState(EnemyController enemy, StateMachine<EnemyController> sm) : base(enemy, sm) 
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void Enter()
    {

    }

    public override void LogicUpdate()
    {

    }
}


