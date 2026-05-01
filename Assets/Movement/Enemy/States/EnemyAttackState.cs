using UnityEngine;

public class EnemyAttackState : State<EnemyController>
{
    public EnemyAttackState(EnemyController enemy, StateMachine<EnemyController> sm) : base(enemy, sm) 
    { 
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        if (!character.CanSeeTarget(character.player, character.attackRange, character.whatIsPlayer))
        {
            stateMachine.ChangeState(character.chaseState);
            return;
        }

        character.agent.SetDestination(character.transform.position);

        if (!character.alreadyAttacked)
        {
            character.skill.UseCurrentSkill();
            character.alreadyAttacked = true;
            character.Invoke(nameof(character.ResetAttack), character.timeBetweenAttacks);
        }
    }
}
