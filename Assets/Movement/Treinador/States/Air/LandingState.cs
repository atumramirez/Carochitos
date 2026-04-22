using UnityEngine;

public class LandingState: State<TrainerController>
{
    float timePassed;
    float landingTime;

    public LandingState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
	{
		character = _character;
		stateMachine = _stateMachine;
	}

    public override void Enter()
	{
		base.Enter();

        timePassed = 0f;


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Landing") && stateInfo.normalizedTime >= 1f)
        {
            Debug.Log("Landing");

            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.standing);
        }
    }
}

