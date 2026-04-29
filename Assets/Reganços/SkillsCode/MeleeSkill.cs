using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill: SkillBase
{
    [Header("Modules")]
    public List<MeleeModule> modules;

    [Header("HurtBox")]
    public HurtBox hurtBox;
}
