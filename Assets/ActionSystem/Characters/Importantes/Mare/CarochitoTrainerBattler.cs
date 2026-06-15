/*
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CarochitoTrainerBattler : CarochitoBattler
{
    public Animator _animator;

    public GameObject discPrefb;

    [Header("Health")]
    public EnemyHealthBar healthBar;

    public bool gettingCaptured = false;

    [Header("Action Graphs")]
    public RuntimeDialogueGraph dialogueGraph;

    [Header("Events")]
    public UnityEvent onInteract;

    public void Start()
    {
        SetUp();
    }

    public override void SetUp()
    {
        Carochito = new Carochito(_base, _level);

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

        Carochito.CurrentHealth -= (int)damage;


        string alert = Carochito.Name + " recebeu " + damage + " de dano";
        AlertManager.instance.AddAlert(Carochito, alert);

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

        string alert = Carochito.Name + " foi derrotado!";
        AlertManager.instance.AddAlert(Carochito, alert);

        _animator.SetTrigger("die");

        alert = attacker.Carochito.Name + " recebeu " + 25 + " de Exp!";
        AlertManager.instance.AddAlert(Carochito, alert);

        attacker.Carochito.GetExp(25);

        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;

        float timer = 0f;

        while (timer < 2f)
        {
            timer += Time.deltaTime;
            float t = timer / 2f;

            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            yield return null;
        }

        transform.localScale = targetScale;

        Destroy(gameObject);

        if (dialogueGraph != null)
        {
            ActionManager.Instance.OpenGraph(dialogueGraph);
        }
    }
}
*/
