using UnityEngine;

public class MonsterLockOnState : State
{
    public MonsterLockOnState(GenericController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
    }

    public override void PhysicsUpdate()
    {
    }
    public override void Exit()
    {
    }
}
