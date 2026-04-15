using UnityEngine;

public class JumpingState: State
{
    bool grounded;

    float gravityValue;
    float jumpHeight;
    float playerSpeed;

    Vector3 airVelocity;

    public JumpingState(TrainerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
	{
		character = _character;
		stateMachine = _stateMachine;
	}

    public override void Enter()
	{
		base.Enter();

		grounded = false;

        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.playerSpeed;
        gravityVelocity.y = 0;

        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");

        Jump();
	}
    
	public override void HandleInput()
	{
		base.HandleInput();
        input = moveAction.ReadValue<Vector2>();
    }

	public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Adcionar Logica de Animação aqui

        if (grounded)
		{
            stateMachine.ChangeState(character.GetComponent<TrainerController>().landing);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!grounded)
        {
            velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0, input.y);

            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0f;

            airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
            airVelocity.y = 0f;

            Vector3 moveDir = airVelocity * character.airControl + velocity * (1 - character.airControl);

            // Mexer no ar
            character.controller.Move(
                gravityVelocity * Time.deltaTime +
                playerSpeed * Time.deltaTime * moveDir
            );

            // Rodar no ar
            moveDir.y = 0f;

            if (moveDir.sqrMagnitude > 0.001f)
            {
                character.transform.rotation = Quaternion.Slerp(
                    character.transform.rotation,
                    Quaternion.LookRotation(moveDir),
                    character.rotationDampTime
                );
            }
        }

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
    }

    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
}

