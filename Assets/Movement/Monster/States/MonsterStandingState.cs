using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterStandingState : State<MonsterController>
{
    float gravityValue;

    Vector3 currentVelocity;
    bool grounded;
    float playerSpeed;

    Vector3 cVelocity;

    public MonsterStandingState(MonsterController _character, StateMachine<MonsterController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;

        character.swap.performed += PressSwap;
        character.ability1.performed += PressSummon;
    }

    private void PressSummon(InputAction.CallbackContext context)
    {
        Debug.Log("Press Atack");
        character.stateMachine.ChangeState(character.attackState);
    }

    private void PressSwap(InputAction.CallbackContext context)
    {
        Debug.Log("Swap button pressed!");
        character.stateMachine.ChangeState(character.swapState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = character.move.ReadValue<Vector2>();

        velocity = new Vector3(input.x, 0, input.y);
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;

        // character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);
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

        character.swap.performed -= PressSwap;
        character.ability1.performed -= PressSummon;
    }
        
}
