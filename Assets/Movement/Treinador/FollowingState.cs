using System.Threading;
using UnityEngine;
public class FollowingState: State<TrainerController>
{
    public float stoppingDistance = 4f;
    public float updateRate = 0.2f;
    private float timer;

    public FollowingState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void LogicUpdate()
    {
        if (character.monster == null) return;

        timer += Time.deltaTime;

        if (timer >= updateRate)
        {
            timer = 0f;

            float distance = Vector3.Distance(character.transform.position, character.monster.transform.position);

            if (distance > stoppingDistance)
            {
                character.navMeshAgent.isStopped = false;
                character.navMeshAgent.SetDestination(character.monster.transform.position);
            }
            else
            {
                character.navMeshAgent.isStopped = true;
            }
        }
    }
}