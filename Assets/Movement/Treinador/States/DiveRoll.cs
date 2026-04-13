using UnityEngine;

public class DiveRollState : State
{
    public DiveRollState(TrainerController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        character.animator.applyRootMotion = true;
        character.animator.SetTrigger("roll");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Dive Roll") && stateInfo.normalizedTime >= 1f)
        {
            Debug.Log("Animation finished");

            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.standing);
        }
    }

    public override void Exit() 
    { 
        base.Exit();
        character.animator.applyRootMotion = false;
    }

}
