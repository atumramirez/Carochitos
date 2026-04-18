using UnityEngine;

/*
public class SummonState : State
{
    public SummonState(GenericController _character, StateMachine _stateMachine) : base(_character, _stateMachine)
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

        // Wait for animation to finish
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
        {
            Debug.Log("Animation finished");

            // Summon
            character.GetComponent<TrainerController>().Summon();

            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.GetComponent<TrainerController>().standing);
        }
    }
}
*/
