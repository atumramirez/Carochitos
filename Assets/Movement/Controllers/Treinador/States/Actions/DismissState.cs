using UnityEngine;

public class DismissState : State<TrainerController>
{
    public DismissState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("enter state: " + this.ToString());

        character.animator.SetTrigger("attack");
    }

    public override void LogicUpdate()
    {
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
        {
            Debug.Log("Dismising animation finished!");

            character.Dismiss();

            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.standing);
        }
    }
}

