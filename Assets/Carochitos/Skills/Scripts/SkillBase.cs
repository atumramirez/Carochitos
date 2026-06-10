using UnityEngine;

public class SkillBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] string _name;

    [TextArea]
    [SerializeField] string _description;

    [Header("Combat Info")]
    [SerializeField] Elemental _elementalType;
    //[SerializeField] ElementalTypes _elementalType;
    [SerializeField] int power;

    [Header("Sprite")]
    [SerializeField] Sprite _skillIcon;

    [Header("Timers")]
    [SerializeField] float _startUpTime;
    [SerializeField] float _cooldown;
    [SerializeField] float _activeTime;

    [Header("Attack Type")]
    [SerializeField] AttackType _attackType;
    
    [Header("Properties")]
    public Elemental Type { get { return _elementalType; } }
    public int Power { get { return power; } }
    public Sprite SkillIcon { get { return _skillIcon;  } }
    public AttackType AttackType { get { return _attackType; } }

    public float FullStartUpTime { get { return _startUpTime; } }
    public float FullCooldown { get { return _cooldown; } }
    public float FullActiveTime { get { return _activeTime; } }

    public virtual void Activate(CarochitoBattler carochitoBattler)
    {
        Debug.Log("Skill Activated");
    }
}

public enum AttackType
{
    Physical,
    Special
}
