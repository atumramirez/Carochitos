/*
using UnityEngine;

public class CarochitoTeamBattler : CarochitoBattler
{
    [Header("Heads Up Display")]
    public HealthBar healthBar;
    public AbilityHolder ability;

    public void SetUp(Carochito carochito, Transform owner = null)
    {
        Carochito = carochito;
        Owner = owner;

        // Health Bar
        healthBar = FindFirstObjectByType<CarochitoHud>().healthBar;
        ability = FindFirstObjectByType<CarochitoHud>().abilityHolder;

        
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(Carochito);
            healthBar.SetHealth(Carochito.CurrentHealth);
        }

        if (ability != null)
        {
            ability.SetUp(Carochito);
        }
        
        
        hurtBox = GetComponentInChildren<HurtBox>();

        if (hurtBox != null)
        {
            hurtBox.SetUp(this);
        }
    }

    public override void TakeDamage(CarochitoBattler attacker, SkillBase skill)
    {
        // Calculate Damage
        DamageCalculator damageCalculator = new();
        float damage = damageCalculator.CaculateDamage(attacker.Carochito, Carochito, skill);

        Carochito.CurrentHealth -= (int)damage;

        // Update UI
        healthBar.SetHealth(Carochito.CurrentHealth);

        if (Carochito.CurrentHealth <= 0)
        {
            Die(attacker);
        }
    }

    public override void Die(CarochitoBattler attacker)
    {
        Carochito.CurrentHealth = 0;
        Carochito.IsAlive = false;

        MonsterController controller = this.GetComponent<MonsterController>();
        controller.stateMachine.ChangeState(controller.swapState);
    }
}
*/
