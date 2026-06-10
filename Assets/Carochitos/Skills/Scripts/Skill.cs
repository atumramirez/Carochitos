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
        StartUp,
        Active,
        Cooldown,
    }

    [SerializeField] SkillState _state = SkillState.Ready;
    [SerializeField] float _startUpTime;
    [SerializeField] float _cooldownTime;
    [SerializeField] float _activeTime;

    public float StartUpTime { get { return _startUpTime; } set { _startUpTime = value; } }
    public float ActiveTime { get { return _activeTime; } set { _activeTime = value; } }
    public float CooldownTime { get { return _cooldownTime; } set { _cooldownTime = value; } }


    public SkillState State { get { return _state; } set { _state = value; } }
}
