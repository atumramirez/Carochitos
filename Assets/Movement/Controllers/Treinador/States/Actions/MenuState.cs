using UnityEngine;
using UnityEngine.InputSystem;

public class MenuState : State<TrainerController>
{
    public MenuState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Confined;
        character.inputManager.menu.action.started += PressMenu;

        character.animator.SetFloat("speed", 0);
    }

    private void PressMenu(InputAction.CallbackContext context)
    {
        character.stateMachine.ChangeState(character.standing);
        character.OpenMenu();
    }

    public override void Exit()
    {
        character.inputManager.menu.action.started -= PressMenu;
    }
}



