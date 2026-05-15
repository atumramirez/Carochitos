using UnityEngine;

[System.Serializable]
public class DamageCalculator
{
    public float CaculateDamage(Carochito attacker, Carochito victim, SkillBase skill)
    {

        // Calcular o nivel do jogador
        float calculateLevel;
        calculateLevel = ((2 * attacker.Level) / 5 );

        float calculatePower;
        calculatePower = (skill.Power * (attacker.Attack / victim.Defense));

        float calculateAbsulutePower;
        calculateAbsulutePower = (calculateLevel * calculatePower) / 50;

        // Calculate Critical Chance
        int criticalCalculator = Random.Range(1, 26);

        float critical = 1;

        if (criticalCalculator == 25)
        {
            critical = 1.5f;
        }

        // Calculate Same Type Attack Bonus
        float stab = 1;

        if (attacker.Base.Type1 == skill.Type || attacker.Base.Type2 == skill.Type)
        {
            stab = 1.5f;
        }

        // Calculate Type Effectiveness 
        ///
        /// For Later
        ///

        float Damage = calculateAbsulutePower * critical + stab;

        Debug.Log(attacker.Name + " atacou " + victim.Name + " com o ataque " + skill.name + " e causou: " + Damage + " de dano.");
        return Damage;
    }
}
