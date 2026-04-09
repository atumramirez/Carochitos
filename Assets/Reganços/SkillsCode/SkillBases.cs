using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillBase", menuName = "Skill/SkillBase")]
public class SkillBases : ScriptableObject
{
    [SerializeField] string _name;

    [TextArea]
    [SerializeField] string _description;

    [SerializeField] ElementalTypes _elementalTypes;

    [SerializeField] int power;

    [Header("Basic")]
    public string skillName;

    [Header("Components")]
    public float coldown;

    [Header("Modules")]
    public List<MeleeModule> modules;
}
