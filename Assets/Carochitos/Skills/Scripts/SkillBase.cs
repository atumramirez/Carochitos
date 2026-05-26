using UnityEngine;

public class SkillBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] string _name;

    [TextArea]
    [SerializeField] string _description;

    [Header("Combat Info")]
    [SerializeField] ElementalTypes _elementalType;
    [SerializeField] int power;

    [Header("Sprite")]
    [SerializeField] Sprite _skillIcon;
    
    [Header("Timers")]
    [SerializeField] int cooldown;
    [SerializeField] int activetime;
    
    [Header("Properties")]
    public ElementalTypes Type { get { return _elementalType; } }
    public int Power { get { return power; } }
    public Sprite SkillIcon { get { return _skillIcon;  } }
    public int Cooldown { get { return cooldown; } }
    public int ActiveTime { get { return activetime; } }

    public virtual void Activate(GameObject parent)
    {
        Debug.Log("Skill Activated");
    }
}
