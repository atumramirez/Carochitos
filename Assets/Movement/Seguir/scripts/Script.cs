using UnityEngine;
using UnityEngine.AI;

public class Following : MonoBehaviour
{
    public Transform player;

    [Header("Follow Settings")]
    public float stoppingDistance = 2f;
    public float updateRate = 0.2f; // how often we update destination

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;

        if (player == null)
        {
            Debug.LogError("Player not assigned!");
        }
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        // Only update destination every few frames (better performance)
        if (timer >= updateRate)
        {
            timer = 0f;

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stoppingDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }
}
