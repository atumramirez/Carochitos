using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;


/*
public class MenuState : State
{
    public MenuState(GenericController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }


    bool menu;
    public override void Enter()
    {
        base.Enter();

        menu = false;
        character.GetComponent<TrainerController>().menuHolder.OpenMenu();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        
        if (menuAction.triggered)
        {
            menu = true;
        }
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (menu)
        {
            stateMachine.ChangeState(character.GetComponent<TrainerController>().standing);
        }
    }


    public override void Exit()
    {
        base.Exit();

        character.GetComponent<TrainerController>().menuHolder.CloseMenu(); ;
    }
}
*/
