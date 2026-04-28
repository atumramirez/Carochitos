using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StandingState: State<TrainerController>
{
    bool grounded;

    float playerSpeed;
    float gravityValue;

    private float airTime;

    Vector3 currentVelocity;
    Vector3 cVelocity;

    public StandingState(TrainerController _character, StateMachine<TrainerController> _stateMachine): base(_character, _stateMachine)
	{
		character = _character;
		stateMachine = _stateMachine;
	}

    public override void Enter()
    {
        base.Enter();

        character.SwitchCamera(character.thirdPersonCam);

        airTime = 0f;

        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;

        character.jump.action.started += PressJump;
        character.crouch.action.started += PressCrouch;
        character.capture.action.started += PressCapture;

        // Summoning and Dismissing the monster
        character.summon.action.started += PressSummon;
        character.dismiss.action.started += PressDismiss;

        character.sprint.action.performed += HeldSprint;

        character.throwin.action.started += PressAim;
    }

    private void PressDismiss(InputAction.CallbackContext context)
    {
        if (character.isMonsterSpawned == true)
        {
            Debug.Log("Dismissig Monster");
            stateMachine.ChangeState(character.dismissing);
        }
    }

    private void PressSummon(InputAction.CallbackContext context)
    {
        if (character.isMonsterSpawned == false)
        {
            Debug.Log("Summonig Monster");
            stateMachine.ChangeState(character.summoning);
        }
        else
        {
            Debug.Log("Swaping Monster");
            stateMachine.ChangeState(character.swaping);
        }
    }

    private void PressCapture(InputAction.CallbackContext context)
    {
        Debug.Log("The Capture Button was pressed");
        stateMachine.ChangeState(character.capturing);
    }

    private void HeldSprint(InputAction.CallbackContext context)
    {
        Debug.Log("You started holding the Sprint button");
        stateMachine.ChangeState(character.sprinting);
    }

    private void PressCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("The Crouch Button was pressed");
        stateMachine.ChangeState(character.crouching);
    }

    private void PressJump(InputAction.CallbackContext context)
    {
        Debug.Log("The Jump Button was pressed");
        stateMachine.ChangeState(character.jumping);  
    }

    private void PressAim(InputAction.CallbackContext context)
    {
        Debug.Log("The Throw Button was pressed");
        stateMachine.ChangeState(character.throwing);
    }

    public override void LogicUpdate() 
    { 
        base.LogicUpdate(); 
        input = character.move.action.ReadValue<Vector2>(); 

        velocity = new Vector3(input.x, 0, input.y); 
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized; 
        velocity.y = 0f; 

        character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime); 
    }
    public override void PhysicsUpdate() 
    { 
        base.PhysicsUpdate(); 

        grounded = character.controller.isGrounded; 
        gravityVelocity.y += character.gravityValue * Time.deltaTime; 

        if (grounded && gravityVelocity.y < 0) 
        { 
            gravityVelocity.y = -2f; 
        } 

        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime); 
        character.controller.Move(character.playerSpeed * Time.deltaTime * currentVelocity + gravityVelocity * Time.deltaTime);
        
        if (velocity.sqrMagnitude > 0.001f) 
        { 
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime); 
        } 
    }

    public override void Exit()
    {
        base.Exit();

        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }

        character.jump.action.started -= PressJump;
        character.crouch.action.started -= PressCrouch;
        character.capture.action.started -= PressCapture;

        character.sprint.action.performed -= HeldSprint;

        character.throwin.action.started -= PressAim;
    }
}
