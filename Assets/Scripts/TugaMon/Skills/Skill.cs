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

    public enum SkillsState
    {
        Ready,
        Active,
        Cooldown,
    }

    [SerializeField] SkillsState _state = SkillsState.Ready;
    public SkillsState State { get { return _state; } set { _state = value; } }

    public virtual void UseSkill()
    {
        //StartCoroutine(StartCooldown());
    }

}
