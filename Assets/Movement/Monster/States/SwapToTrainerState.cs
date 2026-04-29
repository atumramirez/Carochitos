using UnityEngine;


public class MonsterSwapingState : State<MonsterController>
{
    public MonsterSwapingState(MonsterController _character, StateMachine<MonsterController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("enter state: " + this.ToString());

        Debug.Log("Swaping to Trainer!");

        if (character.owner.GetComponent<TrainerController>().isControllingMonster == true)
        {
            character.owner.GetComponent<TrainerController>().SwapToTrainer();
        }
    }
}

