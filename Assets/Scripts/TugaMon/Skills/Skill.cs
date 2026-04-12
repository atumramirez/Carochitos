using UnityEditor;
using UnityEngine;

public class Skill : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] string _name;

    [TextArea]
    [SerializeField] string _description;

    [Header("Combat Info")]
    [SerializeField] ElementalTypes _elementalType;

    [SerializeField] int power;
    public int Power { get { return power; } }

    [Header("Timers")]
    [SerializeField] int cooldown;
    public int Cooldown { get { return cooldown; } }

    [SerializeField] int activetime;
    public int ActiveTime { get { return activetime; } }

    public virtual void Activate(GameObject parent)
    {
        Debug.Log("Skill Activated");
    }
}
