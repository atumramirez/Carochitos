using UnityEngine;

public class MonsterAttackState: State<MonsterController>
{
    public MonsterAttackState(MonsterController _character, StateMachine<MonsterController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        //character.animator.SetTrigger("attack");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        /*
        AnimatorStateInfo stateInfo = character.animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("SardotoAttack") && stateInfo.normalizedTime >= 1f)
        {
            Debug.Log("Attack");
        }
        */
    }

    public override void Exit()
    {
        base.Exit();
    }
}
