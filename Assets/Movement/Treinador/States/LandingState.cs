using UnityEngine;

public class LandingState: State
{
    float timePassed;
    float landingTime;

    public LandingState(TrainerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
	{
		character = _character;
		stateMachine = _stateMachine;
	}

    public override void Enter()
	{
		base.Enter();

        timePassed = 0f;

        // character.animator.SetTrigger("land");
        // landingTime = 0f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

		if (timePassed > landingTime)
		{
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.GetComponent<TrainerController>().standing);
        }

        timePassed += Time.deltaTime;
    }
}

