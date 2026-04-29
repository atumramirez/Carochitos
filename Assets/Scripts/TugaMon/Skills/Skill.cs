using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    [SerializeField] SkillBase _base;
    public SkillBase Base { get { return _base; } }

    public Skill(SkillBase chitoBase)
    {
        _base = chitoBase;
    }

    public enum SkillState
    {
        Ready,
        Active,
        Cooldown,
    }

    [SerializeField] SkillState _state = SkillState.Ready;
    public SkillState State { get { return _state; } set { _state = value; } }

    public virtual void UseSkill()
    {
    }

}
