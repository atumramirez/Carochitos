using UnityEngine;

public class CarochitoEnemyBattler : CarochitoBattler
{
    public Animator _animator;

    [Header("Health")]
    public EnemyHealthBar healthBar;

    public void Start()
    {
        SetUp();
    }

    public override void SetUp()
    {
        Carochito = new Carochito( _base, _level);

        hurtBox = GetComponentInChildren<HurtBox>();

        if (hurtBox != null)
        {
            hurtBox.SetUp(this);
        }

        healthBar = GetComponentInChildren<EnemyHealthBar>();

        if (healthBar != null)
        {
            healthBar.SetName(Carochito.Name);
            healthBar.SetMaxHealth(Carochito);
            healthBar.SetHealth(Carochito.CurrentHealth);
        }

    }

    public override void TakeDamage(CarochitoBattler attacker, SkillBase skill)
    {
        DamageCalculator damageCalculator = new();
        float damage = damageCalculator.CaculateDamage(attacker.Carochito, Carochito, skill);

        Carochito.CurrentHealth -= (int) damage;


        if (healthBar != null)
        {
            healthBar.SetHealth(Carochito.CurrentHealth);
        }

        if (Carochito.CurrentHealth <= 0)
        {
            Die(attacker);
        }
    }

    public override void Die(CarochitoBattler attacker)
    {
        Carochito.CurrentHealth = 0;
        Carochito.IsAlive = false;

        attacker.Carochito.GetExp(25);

        _animator.SetTrigger("die");
    }

    public override void Capture()
    {
        Destroy(gameObject);
    }
}
