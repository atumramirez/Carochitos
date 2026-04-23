using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public UseSkill skill;

    public float sightRange, attackRange;
    public bool playerInSightRnage, playerInAttackRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        playerInSightRnage = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); ;
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRnage && !playerInAttackRange) Patroling();
        if (playerInSightRnage && !playerInAttackRange) ChasePlayer();
        if (playerInSightRnage && playerInAttackRange) AttackPlayer();

    }
    private void Patroling()
    {
        if (!walkPointSet) Search();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            Vector3 distanceToWalk = transform.position - walkPoint;    

            if(distanceToWalk.magnitude<1f)
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
        if(Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround))
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
        skill.UseCurrentSkill();
    }
    // Update is called once per frame
    
}
