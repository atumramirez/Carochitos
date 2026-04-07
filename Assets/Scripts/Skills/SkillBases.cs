using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillBase", menuName = "Skill/SkillBase")]
public class SkillBases : ScriptableObject
{
    [Header("Basic")]
    public string skillName;

    [Header("Components")]
    public List<MeleeModule> modules;
}
