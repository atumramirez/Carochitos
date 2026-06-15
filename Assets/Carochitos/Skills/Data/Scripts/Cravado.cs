using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Carochito/Skill/Cravado")]

public class Cravado : SkillBase
{
    public GameObject projectilePrefab;

    public override void Activate(CarochitoBattler carochitoBattler)
    {
        GameObject projectile = Instantiate(projectilePrefab, carochitoBattler._firePoint.position, carochitoBattler._firePoint.rotation);

        if (projectile.TryGetComponent<HitBox>(out var hitBox))
        {
            hitBox.SetUp(carochitoBattler, this);
        }

    }
}