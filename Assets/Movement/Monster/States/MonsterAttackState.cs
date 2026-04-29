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

        character.handler.carochito.Skills[0].Base.Activate(character.gameObject);

        character.stateMachine.ChangeState(character.standingState);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        /*
        switch (_state)
        {
            case SkillState.Active:

                if (_activeTime > 0)
                {
                    _activeTime -= Time.deltaTime;
                }
                else
                {
                    _state = SkillState.Cooldown;
                    cooldown.StartCooldown();
                    _cooldownTime = _skills[0].Cooldown;
                }
                break;

            case SkillState.Cooldown:
                if (_cooldownTime > 0)
                {
                    _cooldownTime -= Time.deltaTime;
                }
                else
                {
                    _state = SkillState.Ready;
                }
                break;
        }
        */
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
