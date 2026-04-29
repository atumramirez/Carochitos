using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Skill;

public class MonsterAttackState: State<MonsterController>
{
    float _cooldownTime;
    float _activeTime;

    public MonsterAttackState(MonsterController _character, StateMachine<MonsterController> _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Attacaste");
    }

    public override void Exit()
    {
        base.Exit();
    }
}
