using UnityEngine;

public class FallingState : State<TrainerController>
{
    private bool grounded;
    private Vector3 airVelocity;

    public FallingState(TrainerController _character, StateMachine<TrainerController> _stateMachine)
        : base(_character, _stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        grounded = false;
        gravityVelocity.y = 0;

        // character.animator.SetBool("isFalling", true);
    }

    public override void Exit()
    {
        base.Exit();

        // character.animator.SetBool("isFalling", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = character.move.action.ReadValue<Vector2>();

        if (grounded)
        {
            character.animator.SetTrigger("land");
            stateMachine.ChangeState(character.landing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector3 velocity = character.playerVelocity;

        airVelocity = new Vector3(input.x, 0, input.y);

        // Camera-relative movement
        velocity = velocity.x * character.cameraTransform.right.normalized +
                   velocity.z * character.cameraTransform.forward.normalized;

        airVelocity = airVelocity.x * character.cameraTransform.right.normalized +
                      airVelocity.z * character.cameraTransform.forward.normalized;

        velocity.y = 0f;
        airVelocity.y = 0f;

        Vector3 moveDir = airVelocity * character.airControl +
                          velocity * (1 - character.airControl);

        character.controller.Move(
            gravityVelocity * Time.deltaTime +
            character.playerSpeed * Time.deltaTime * moveDir
        );

        // Rotate toward movement
        if (moveDir.sqrMagnitude > 0.001f)
        {
            character.transform.rotation = Quaternion.Slerp(
                character.transform.rotation,
                Quaternion.LookRotation(moveDir),
                character.rotationDampTime
            );
        }

        // Apply gravity
        gravityVelocity.y += character.gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;

    }
}
