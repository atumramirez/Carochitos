using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsObstacle;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public UseSkill skill;

    public float sightRange, attackRange;
    public float viewAngle = 90f;

    public Transform ball;
    public LayerMask whatIsBall;

    public float investigationTime = 3f;
    private float investigateTimer;
    private bool investigatingBall;

    public bool playerInSightRnage, playerInAttackRange;

    public void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FindBall();
        
        bool playerInSphere = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        bool playerVisible = playerInSphere && IsInFieldOfView(player.position) && HasLineOfSight(player);

        bool playerInAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer)
                              && IsInFieldOfView(player.position)
                              && HasLineOfSight(player);

        
        bool ballInSphere = ball != null && Physics.CheckSphere(transform.position, sightRange, whatIsBall);
        bool ballVisible = ballInSphere && IsInFieldOfView(ball.position) && HasLineOfSight(ball);

        

        if (playerVisible)
        {
            investigatingBall = false;

            if (playerInAttack)
                AttackPlayer();
            else
                ChasePlayer();
        }
        else if (ballVisible)
        {
            investigatingBall = true;
            investigateTimer = investigationTime;
            InvestigateBall();
        }
        else if (investigatingBall)
        {
            InvestigateBall();
        }
        else
        {
            Patroling();
        }
    }

    void FindBall()
    {
        if (ball != null) return;

        GameObject ballF = GameObject.FindGameObjectWithTag("Bola");

        if (ballF != null)
        {
            ball = ballF.transform;
        }
    }

    bool IsInFieldOfView(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, direction);
        return angle < viewAngle / 2f;
    }

    bool HasLineOfSight(Transform target)
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 direction = (target.position - origin).normalized;
        float distance = Vector3.Distance(origin, target.position);

        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.green);

            if (hit.transform == target)
                return true;
        }

        return false;
    }

    void InvestigateBall()
    {
        if (ball == null) return;

        agent.SetDestination(ball.position);

        investigateTimer -= Time.deltaTime;

        if (investigateTimer <= 0f)
        {
            investigatingBall = false;
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) Search();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            Vector3 distanceToWalk = transform.position - walkPoint;

            if (distanceToWalk.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }
    }

    private void Search()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            skill.UseCurrentSkill();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftBoundary * sightRange);
        Gizmos.DrawRay(transform.position, rightBoundary * sightRange);
    }
}