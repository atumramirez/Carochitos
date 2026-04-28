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

    public bool playerInSightRnage, playerInAttackRange;

    public void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        bool inSightSphere = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        bool inAttackSphere = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        bool inFOV = IsInFieldOfView();
        bool hasLOS = HasLineOfSight();

        playerInSightRnage = inSightSphere && inFOV && hasLOS;
        playerInAttackRange = inAttackSphere && inFOV && hasLOS;

        if (!playerInSightRnage && !playerInAttackRange) Patroling();
        if (playerInSightRnage && !playerInAttackRange) ChasePlayer();
        if (playerInSightRnage && playerInAttackRange) AttackPlayer();
    }

    bool IsInFieldOfView()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        return angle < viewAngle / 2f;
    }

    bool HasLineOfSight()
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 direction = (player.position - origin).normalized;
        float distance = Vector3.Distance(origin, player.position);

        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);

            if (hit.transform == player)
                return true;
        }

        return false;
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