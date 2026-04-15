using UnityEngine;
using UnityEngine.InputSystem;

public class State
{
    public GenericController character;
    public StateMachine stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;
    public InputAction crouchAction;
    public InputAction sprintAction;
    public InputAction attackAction;
    public InputAction rollAction;
    public InputAction summonAction;
    public InputAction changeAction;

    [Header("Monster Controls")]
    public InputAction switchAction;

    public State(GenericController _character, StateMachine _stateMachine)
	{
        character = _character;
        stateMachine = _stateMachine;

        var playerMap = character.playerInput.actions.FindActionMap("Trainer");
        var monsterMap = character.playerInput.actions.FindActionMap("Monster");

        playerMap.Enable();
        monsterMap.Enable();

        moveAction = character.playerInput.actions["Move"];
        jumpAction = character.playerInput.actions["Jump"];
        crouchAction = character.playerInput.actions["Crouch"];
        sprintAction = character.playerInput.actions["Sprint"];
        attackAction = character.playerInput.actions["Attack"];
        rollAction = character.playerInput.actions["Roll"];
        summonAction = character.playerInput.actions["Summon"];
        changeAction = character.playerInput.actions["Change"];

        // Monster
        switchAction = character.playerInput.actions["Switch"];
    }

    public virtual void Enter()
    {
        Debug.Log("enter state: " + this.ToString());
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}

