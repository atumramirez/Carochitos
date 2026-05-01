using UnityEngine;

public class InvestigateState : State<EnemyController>
{
    private float timer;

    public InvestigateState(EnemyController enemy, StateMachine<EnemyController> sm) : base(enemy, sm) { }

    public override void Enter()
    {
        base.Enter();
        timer = 3f;
    }

    public override void LogicUpdate()
    {
        if (character.ball == null)
        {
            stateMachine.ChangeState(character.patrolState);
            return;
        }

        if (character.CanSeeTarget(character.player, character.sightRange, character.whatIsPlayer))
        {
            stateMachine.ChangeState(character.chaseState);
            return;
        }

        character.agent.SetDestination(character.ball.position);

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            stateMachine.ChangeState(character.patrolState);
        }
    }
}
