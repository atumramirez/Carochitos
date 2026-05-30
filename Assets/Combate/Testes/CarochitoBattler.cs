using UnityEngine;

public class CarochitoBattler : MonoBehaviour
{
    [SerializeField] CarochitoBase _base;
    [SerializeField] int _level;

    [Header("Carochito")]
    public Carochito Carochito;

    [Header("HurtBox")]
    public HurtBox hurtBox;

    public void Start()
    {
        SetUp();
    }

    public void SetUp()
    {
        Carochito = new Carochito( _base, _level);

        hurtBox = GetComponent<HurtBox>();

        if (hurtBox != null)
        {
            hurtBox.SetUp(this);
        }
    }

    public void TakeDamage(CarochitoBattler attacker, SkillBase skill)
    {
        DamageCalculator damageCalculator = new();
        float damage = damageCalculator.CaculateDamage(attacker.Carochito, Carochito, skill);

        Carochito.CurrentHealth -= (int) damage;

        if (Carochito.CurrentHealth <= 0)
        {
            Die(attacker);
        }
    }

    public void Die(CarochitoBattler attacker)
    {
        Carochito.CurrentHealth = 0;
        Carochito.IsAlive = false;

        attacker.Carochito.GetExp(25);
    }
}
