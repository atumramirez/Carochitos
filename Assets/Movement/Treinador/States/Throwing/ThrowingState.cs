using UnityEngine;

public class ThrowingState : State<TrainerController>
{
    bool grounded;

    float playerSpeed;
    float gravityValue;

    private float airTime;

    Vector3 currentVelocity;
    Vector3 cVelocity;

    public ThrowingState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = character.move.action.ReadValue<Vector2>();

        // Flatten camera directions (ignore vertical tilt)
        Vector3 camForward = character.cameraTransform.forward;
        Vector3 camRight = character.cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // Camera-relative movement
        velocity = camRight * input.x + camForward * input.y;

        // Animator
        character.animator.SetFloat
        (
            "speed",
            input.magnitude,
            character.speedDampTime,
            Time.deltaTime
        );
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        grounded = character.controller.isGrounded;

        // Gravity
        gravityVelocity.y += character.gravityValue * Time.deltaTime;

        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        // Smooth movement
        currentVelocity = Vector3.SmoothDamp(
            currentVelocity,
            velocity,
            ref cVelocity,
            character.velocityDampTime
        );

        character.controller.Move
        (
            character.playerSpeed * Time.deltaTime * currentVelocity +
            gravityVelocity * Time.deltaTime
        );

        // -------- ROTATION LOGIC (TPS STYLE) --------

        // Face camera forward while aiming
        Vector3 aimDirection = character.cameraTransform.forward;
        aimDirection.y = 0f;

        if (aimDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection);

            character.transform.rotation = Quaternion.Slerp
            (
                character.transform.rotation,
                targetRotation,
                character.rotationDampTime
            );
        }
    }
}
