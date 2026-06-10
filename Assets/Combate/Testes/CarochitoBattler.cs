using UnityEngine;

public class CarochitoBattler : MonoBehaviour
{
    [HideInInspector] public CarochitoBase _base;
    [Range(1, 100)]
    [HideInInspector] public int _level;

    [Header("Carochito")]
    public Carochito Carochito;

    [Header("HurtBox")]
    public HurtBox hurtBox;

    [Header("Fire Point")]
    public Transform firePoint;

    [Header("Owner")]
    public Transform Owner;

    public virtual void SetUp()
    {
        Carochito = new Carochito( _base, _level);

        hurtBox = GetComponent<HurtBox>();

        if (hurtBox != null)
        {
            hurtBox.SetUp(this);
        }
    }

    public virtual void TakeDamage(CarochitoBattler attacker, SkillBase skill)
    {
        DamageCalculator damageCalculator = new();
        float damage = damageCalculator.CaculateDamage(attacker.Carochito, Carochito, skill);

        Carochito.CurrentHealth -= (int) damage;

        if (Carochito.CurrentHealth <= 0)
        {
            Die(attacker);
        }
    }

    public virtual void Die(CarochitoBattler attacker)
    {
        Carochito.CurrentHealth = 0;
        Carochito.IsAlive = false;

        attacker.Carochito.GetExp(25);
    }

    public virtual void Capture()
    {
        Destroy(gameObject);
    }
}
