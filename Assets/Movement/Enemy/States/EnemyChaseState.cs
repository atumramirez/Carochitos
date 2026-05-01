using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyChaseState : State<EnemyController>
{

    public EnemyChaseState(EnemyController enemy, StateMachine<EnemyController> sm) : base(enemy, sm) 
    { 
    }


    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (!character.CanSeeTarget(character.player, character.sightRange, character.whatIsPlayer))
        {
            stateMachine.ChangeState(character.patrolState);
            return;
        }

        if (character.CanSeeTarget(character.player, character.attackRange, character.whatIsPlayer))
        {
            stateMachine.ChangeState(character.attackState);
            return;
        }

        character.agent.SetDestination(character.player.position);
    }
}
