using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill: Skill
{
    [Header("Modules")]
    public List<MeleeModule> modules;

    [Header("HurtBox")]
    public HurtBox hurtBox;
}
