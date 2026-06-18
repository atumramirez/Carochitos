using UnityEngine;

public class CaptureState : State<TrainerController>
{
    public CaptureState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        character.StartCapture();
        character.animator.SetTrigger("attack");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
        {
            Debug.Log("Animation finished");

            character.EndCapture();
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.standing);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

