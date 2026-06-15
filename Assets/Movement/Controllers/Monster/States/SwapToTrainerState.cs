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

        Debug.Log("Swaping to Trainer!");
        if (character.carochitoBattler._owner.isControllingMonster == true)
        {
            character.carochitoBattler._owner.SwapToTrainer();
        }
    }
}

