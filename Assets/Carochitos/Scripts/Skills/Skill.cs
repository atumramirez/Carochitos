using UnityEngine;

public class Skill
{
    public SkillBase Base { get; set; }

    public Skill(SkillBase chitobase)
    {
        Base = chitobase;
    }
}
