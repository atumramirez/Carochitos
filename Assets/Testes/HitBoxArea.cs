using UnityEngine;

public class HitBoxArea : MonoBehaviour
{
    public CarochitoBattler BattlerOwner;
    public SkillBase SkillUsed;
    public bool dealtDamage=false;
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
        Debug.Log("Acertou em Algo");
        if (other.TryGetComponent<HurtBox>(out var hurtbox))
        {
            CarochitoBattler victim = hurtbox.CarochitoBattler;

            if (victim.Carochito.IsAlive == true && !dealtDamage)
            {
                hurtbox.ReceiveHit(BattlerOwner, SkillUsed);
                dealtDamage = true;
            }
        }
    }
}
