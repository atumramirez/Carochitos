using UnityEngine;
using UnityEngine.AI;

public class MonsterFollowState : State<MonsterController>
{
    public float stoppingDistance = 4f;
    public float updateRate = 0.2f;
    private float timer;

    public MonsterFollowState(MonsterController _character, StateMachine<MonsterController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (character.owner == null) return;

        timer += Time.deltaTime;

        if (timer >= updateRate)
        {
            timer = 0f;

            float distance = Vector3.Distance(character.transform.position, character.owner.position);

            if (distance > stoppingDistance)
            {
                character.navMeshAgent.isStopped = false;
                character.navMeshAgent.SetDestination(character.owner.position);
            }
            else
            {
                character.navMeshAgent.isStopped = true;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
