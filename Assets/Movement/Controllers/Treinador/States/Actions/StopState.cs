using UnityEngine;
using UnityEngine.InputSystem;

public class StopState : State<TrainerController>
{
    public StopState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Confined;

        character.animator.SetFloat("speed", 0);
    }

    public override void Exit()
    {
        //character.inputManager.menu.action.started -= PressMenu;
    }
}



