using UnityEngine;
using UnityEngine.InputSystem;

public class StandingState: State<TrainerController>
{
    bool grounded;

    float playerSpeed;
    float gravityValue;

    //private float airTime;

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

        Cursor.lockState = CursorLockMode.Locked;

        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;

        character.inputManager.jump.action.started += PressJump;
        character.inputManager.crouch.action.started += PressCrouch;
        
        character.inputManager.summon.action.started += PressSummon;
        character.inputManager.dismiss.action.started += PressDismiss;

        character.inputManager.sprint.action.performed += HeldSprint;

        character.inputManager.capture.action.started += PressCapture;
        character.inputManager.throwin.action.started += PressAim;

        character.inputManager.menu.action.started += PressMenu;

        character.inputManager.interact.action.started += PressInteract;

        character.inputManager.next.action.started += PressNext;
        character.inputManager.previous.action.started += PressPrevious;

    }

    private void PressPrevious(InputAction.CallbackContext context)
    {
        character.PreviousCarochito();
    }

    private void PressNext(InputAction.CallbackContext context)
    {
        character.NextCarochito();
    }

    private void PressInteract(InputAction.CallbackContext context)
    {
        character.Interact();
    }

    private void PressMenu(InputAction.CallbackContext context)
    {
        character.stateMachine.ChangeState(character.menu);
        character.OpenMenu();
    }

    private void PressDismiss(InputAction.CallbackContext context)
    {
        if (character.sceneManager._currentEnviroment == Enviroment.Outiside)
        {
            if (character.isMonsterSpawned == true)
            {
                stateMachine.ChangeState(character.dismissing);
            }
        }
    }

    private void PressSummon(InputAction.CallbackContext context)
    {
        if (character.sceneManager._currentEnviroment == Enviroment.Outiside)
        {
            if (character.party.partyCarochitos.Count > 0)
            {
                if (character.isMonsterSpawned == false)
                {
                    stateMachine.ChangeState(character.summoning);
                }
                else
                {
                    stateMachine.ChangeState(character.swaping);
                }
            }
        }
    }

    private void PressCapture(InputAction.CallbackContext context)
    {
        if (character.playerInfo.hasHammer == true)
        {
            stateMachine.ChangeState(character.capturing);
        }
        
    }

    private void HeldSprint(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(character.sprinting);
    }

    private void PressCrouch(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(character.crouching);
    }

    private void PressJump(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(character.jumping);  
    }

    private void PressAim(InputAction.CallbackContext context)
    {
        if (character.sceneManager._currentEnviroment == Enviroment.Outiside)
        {
            stateMachine.ChangeState(character.throwing);
        }
    }

    public override void LogicUpdate() 
    { 
        base.LogicUpdate(); 
        input = character.inputManager.move.action.ReadValue<Vector2>(); 

        velocity = new Vector3(input.x, 0, input.y); 
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized; 
        velocity.y = 0f; 

        character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);
        character.noiseArea.ChangeNoiseLevel(input.magnitude);
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

        character.inputManager.jump.action.started -= PressJump;
        character.inputManager.crouch.action.started -= PressCrouch;
        character.inputManager.capture.action.started -= PressCapture;

        character.inputManager.summon.action.started -= PressSummon;
        character.inputManager.dismiss.action.started -= PressDismiss;

        character.inputManager.sprint.action.performed -= HeldSprint;

        character.inputManager.throwin.action.started -= PressAim;

        character.inputManager.menu.action.started -= PressMenu;

        character.inputManager.interact.action.started -= PressInteract;

        character.inputManager.next.action.started -= PressNext;
        character.inputManager.previous.action.started -= PressPrevious;
    }
}
