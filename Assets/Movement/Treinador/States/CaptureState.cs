using UnityEditor;
using UnityEngine;

/*
public class CaptureState : State
{
    public CaptureState(TrainerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        character.animator.SetTrigger("attack");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
        {
            Debug.Log("Animation finished");

            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.GetComponent<TrainerController>().standing);
        }
    }
}
*/
