/*
using UnityEngine;
using System.Collections;
using System;

public class CarochitoEnemyBattler : CarochitoBattler
{
    public Animator _animator;

    public GameObject discPrefb;

    [Header("Health")]
    public EnemyHealthBar healthBar;

    public bool gettingCaptured = false;

    public void SetUp(CarochitoBase Base, int Level)
    {
        /*
        Carochito = new Carochito(Base, Level);

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


    private Action onDeath;
    public void Setup(Action deathCallback)
    {
        onDeath = deathCallback;
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

        
        if (dialogueGraph != null)
        {
            ActionManager.Instance.OpenGraph(dialogueGraph);
        }
        

        Destroy(gameObject);
        onDeath?.Invoke();
    }

    public override void Capture()
    {
        gettingCaptured = true;
        StartCoroutine(CaptureRoutine());
    }

    private IEnumerator CaptureRoutine()
    {
        _animator.SetTrigger("takeDamage");

        yield return null;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(stateInfo.length);

        GameObject discObject = Instantiate(
            discPrefb,
            capturePoint.position,
            capturePoint.rotation
        );

        if (discObject.TryGetComponent<DiscCapture>(out var disc))
        {
            disc.SetUp(Carochito, gameObject);
        }

        Destroy(gameObject);
    }
}
*/
