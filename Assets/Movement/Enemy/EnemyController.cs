using UnityEngine;
using UnityEngine.AI;

public class EnemyController: GenericController
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform ball;

    public LayerMask whatIsGround, whatIsPlayer, whatIsBall;

    public float sightRange, attackRange, viewAngle = 90f;
    public float walkPointRange;
    public float timeBetweenAttacks;

    public UseSkill skill;

    [HideInInspector] public bool alreadyAttacked;

    public StateMachine<EnemyController> stateMachine;

    // States
    public EnemyPatrolState patrolState;
    public EnemyChaseState chaseState;
    public EnemyAttackState attackState;
    public InvestigateState investigateState;

    private void Start()
    {

        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine<EnemyController>();

        patrolState = new EnemyPatrolState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        investigateState = new InvestigateState(this, stateMachine);

        stateMachine.Initialize(patrolState);
    }

    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public bool CanSeeTarget(Transform target, float range, LayerMask mask)
    {
        if (target == null) return false;

        bool inRange = Physics.CheckSphere(transform.position, range, mask);
        if (!inRange) return false;

        Vector3 dir = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dir);

        if (angle > viewAngle / 2f) return false;


        if (Physics.Raycast(transform.position + Vector3.up, dir, out RaycastHit hit, range))
        {
            return hit.transform == target;
        }

        return false;
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
    }

}