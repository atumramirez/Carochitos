using UnityEngine;

public class SummonState : State<TrainerController>
{
    public SummonState(TrainerController _character, StateMachine<TrainerController> _stateMachine) : base(_character, _stateMachine)
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
            Debug.Log("Summoning animation finished!");

            character.Summon();

            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.standing);
        }
    }
}

