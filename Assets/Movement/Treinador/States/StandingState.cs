using UnityEngine;

public class StandingState: State
{
    float gravityValue;

    // Ações
    bool jump;   
    bool crouch;
    bool attack;
    bool roll;
    bool sprint;
    bool summon;
    bool change;
    bool menu;

    Vector3 currentVelocity;
    bool grounded;
    float playerSpeed;

    Vector3 cVelocity;

    public StandingState(TrainerController _character, StateMachine _stateMachine): base(_character, _stateMachine)
	{
		character = _character;
		stateMachine = _stateMachine;
	}

    public override void Enter()
    {
        base.Enter();

        jump = false;
        crouch = false;
        sprint = false;
        attack = false;
        roll = false;
        summon = false;
        change = false;
        menu = false;

        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;    
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (jumpAction.triggered)
        {
            jump = true;
		}
		if (crouchAction.triggered)
		{
            crouch = true;
		}
		if (sprintAction.triggered)
		{
            sprint = true;
		}
        if (attackAction.triggered)
        {
            attack = true;
        }
        if (rollAction.triggered)
        { 
            roll = true;
        }
        if (summonAction.triggered)
        {
            summon = true;
        }
        if (changeAction.triggered)
        {
            change = true;
        }
        if (menuAction.triggered)
        {
            menu = true;
        }

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);

        if (sprint)
		{
            stateMachine.ChangeState(character.GetComponent<TrainerController>().sprinting);
        }    
        if (jump)
        {
            stateMachine.ChangeState(character.GetComponent<TrainerController>().jumping);
        }
		if (crouch)
		{
            stateMachine.ChangeState(character.GetComponent<TrainerController>().crouching);
        }
        if (attack)
        {
            stateMachine.ChangeState(character.GetComponent<TrainerController>().capture);
        }
        if (roll)
        {
            stateMachine.ChangeState(character.GetComponent<TrainerController>().diveRoll);
        }
        if (summon)
        {
            stateMachine.ChangeState(character.GetComponent<TrainerController>().summonState);
        }
        if (change)
        {
            stateMachine.ChangeState(character.GetComponent<TrainerController>().changeCharacterState);
        }
        if (menu)
        {
            stateMachine.ChangeState(character.GetComponent<TrainerController>().menuState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;

        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }
       
        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move(playerSpeed * Time.deltaTime * currentVelocity + gravityVelocity * Time.deltaTime);
  
		if (velocity.sqrMagnitude > 0)
		{
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity),character.rotationDampTime);
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
    }

}
