using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public CarochitoBattler BattlerOwner;
    public SkillBase SkillUsed;

    // Diferent types of Targets
    public Target target = Target.Enemy;
    public enum Target
    {
        Enemy,
        Ally
    }

    public void SetUp(CarochitoBattler carochitoBattler, SkillBase skill)
    {
        BattlerOwner = carochitoBattler;
        SkillUsed = skill;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HurtBox>(out var hurtbox))
        {
            
            CarochitoBattler victim = hurtbox.CarochitoBattler;

            if (victim.Carochito.IsAlive == true)
            {
                hurtbox.ReceiveHit(BattlerOwner, SkillUsed);
            } 
        }
    }
}
