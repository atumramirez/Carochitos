using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchingState: State<TrainerController>
{
    float playerSpeed;
    bool belowCeiling;

    bool grounded;
    float gravityValue;
    Vector3 currentVelocity;

    public CrouchingState(TrainerController _character, StateMachine<TrainerController> _stateMachine):base(_character, _stateMachine)
	{
		character = _character;
		stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        character.animator.SetTrigger("crouch");  

        belowCeiling = false;
        gravityVelocity.y = 0;

        playerSpeed = character.crouchSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;

        character.crouch.action.started += HeldCrouch;
        character.jump.action.started += PressJump;
    }

    private void PressJump(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(character.roll);
    }

    private void HeldCrouch(InputAction.CallbackContext context)
    {
        if (!belowCeiling)
        {
            stateMachine.ChangeState(character.standing);
        }
    }

    public override void Exit()
    {
        base.Exit();

        character.animator.SetTrigger("move");

        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x, 0, input.y);

        character.crouch.action.started -= HeldCrouch;
        character.jump.action.started -= PressJump;
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

        belowCeiling = CheckCollisionOverlap(character.transform.position + Vector3.up * character.normalColliderHeight);

        gravityVelocity.y += gravityValue * Time.deltaTime;

        grounded = character.controller.isGrounded;

        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        currentVelocity = Vector3.Lerp(currentVelocity, velocity, character.velocityDampTime);

        character.controller.Move(playerSpeed * Time.deltaTime * currentVelocity + gravityVelocity * Time.deltaTime);

        if (velocity.magnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }

    public bool CheckCollisionOverlap(Vector3 targetPositon)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        Vector3 direction = targetPositon - character.transform.position;

        if (Physics.Raycast(character.transform.position, direction, out RaycastHit hit, character.normalColliderHeight, layerMask))
        {
            Debug.DrawRay(character.transform.position, direction * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(character.transform.position, direction * character.normalColliderHeight, Color.white);
            return false;
        }       
    }
}

