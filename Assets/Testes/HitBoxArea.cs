using System.Collections.Generic;
using UnityEngine;

public class HitBoxArea : MonoBehaviour
{
    public CarochitoBattler BattlerOwner;
    public SkillBase SkillUsed;
    public bool dealtDamage=false;
    // Diferent types of Targets
    public Target target = Target.Enemy;
    public GameObject particles;
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
    public void GetEffect(GameObject fEffect)
    {
        particles = fEffect;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Acertou em Algo");
        if (other.TryGetComponent<HurtBox>(out var hurtbox))
        {
            CarochitoBattler victim = hurtbox.CarochitoBattler;
            
            GameObject projectile = Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(projectile, 5f);
            if (victim.Carochito.IsAlive == true && !dealtDamage)
            {

                hurtbox.ReceiveHit(BattlerOwner, SkillUsed);
                dealtDamage = true;
            }
        }
    }
}
