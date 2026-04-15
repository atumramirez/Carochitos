using UnityEngine;

public class SwapToTrainerState : State
{
    public SwapToTrainerState(GenericController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        character.GetComponent<MonsterController>().characterHandler.SwapCharacter();
    }
}
