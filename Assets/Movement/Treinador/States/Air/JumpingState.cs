using UnityEngine;

public class JumpingState: State<TrainerController>
{
    private Vector3 airVelocity;

    public JumpingState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
	{
		character = _character;
		stateMachine = _stateMachine;
	}

    public override void Enter()
    {
        base.Enter();

        gravityVelocity.y = 0;

        gravityVelocity.y += Mathf.Sqrt
            (
                character.jumpHeight * -3.0f * character.gravityValue
            );


        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = character.move.action.ReadValue<Vector2>();
        
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (gravityVelocity.y < 0)
        {
            character.animator.SetTrigger("fall");
            stateMachine.ChangeState(character.falling);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector3 velocity = character.playerVelocity;

        airVelocity = new Vector3(input.x, 0, input.y);

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
    }
}


