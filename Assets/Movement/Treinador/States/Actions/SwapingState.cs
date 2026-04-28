using UnityEngine;


public class SwapingState : State<TrainerController>
{
    public SwapingState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("enter state: " + this.ToString());

        if (character.isControllingMonster == true)
        {
            character.SwapToTrainer();
        }
        else
        {
            character.SwapToMonster();
        }
    }
}

