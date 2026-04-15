using UnityEngine;

public class SwapToMonsterState : State
{
    public SwapToMonsterState(GenericController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("enter state: " + this.ToString());

        character.GetComponent<TrainerController>().Swap();
    }
}
