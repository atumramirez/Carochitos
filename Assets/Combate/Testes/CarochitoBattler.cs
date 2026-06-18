using UnityEngine;
using System.Collections;
using System;

public class CarochitoBattler : MonoBehaviour
{
    [Header("Carochito")]
    public Carochito Carochito;

    [Header("Info")]
    public bool _isEnemy = false;
    
    [Header("Controller")]
    public MonsterController _controller;

    [Header("Capture")]
    public bool _isCapturable = true;
    public bool _isGettingCaptured;
    public Transform _capturePoint;
    public GameObject _diskPrefab;

    [Header("Owner")]
    public TrainerController _owner;

    [Header("Fire Point")]
    public Transform _firePoint;

    [Header("Heads Up Display")]
    public HealthBar _allyHealthBar;
    public AbilityHolder _allyAbilities;
    public EnemyHealthBar _enemyHealthBar;

    [Header("HurtBox")]
    public HurtBox _hurtBox;

    public void SetUp(Carochito _carochito, bool isEnemy = true, bool isCapturable = true, TrainerController owner = null)
    {
        Carochito = _carochito;

        _isEnemy = isEnemy;
        _isCapturable = isCapturable;
        _owner = owner;

        // Health
        _enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();

        if (_isEnemy == true)
        {
            if (_enemyHealthBar != null)
            {
                _enemyHealthBar.enabled = true;
                _enemyHealthBar.SetName(Carochito.Name);
                _enemyHealthBar.SetMaxHealth(Carochito);
                _enemyHealthBar.SetHealth(Carochito.CurrentHealth);
            }
        }
        else
        {
            if (_enemyHealthBar != null)
            {
                _enemyHealthBar.enabled = false;
            }

            _allyHealthBar = FindFirstObjectByType<CarochitoHud>().healthBar;
            _allyAbilities = FindFirstObjectByType<CarochitoHud>().abilityHolder;

            if (_allyHealthBar != null)
            {
                _allyHealthBar.SetMaxHealth(Carochito);
                _allyHealthBar.SetHealth(Carochito.CurrentHealth);
            }

            if (_allyAbilities != null)
            {
                _allyAbilities.SetUp(Carochito);
            }
        }
    
        // Hurt Box
        if (_hurtBox == null)
        {
            _hurtBox = GetComponent<HurtBox>();

            if (_hurtBox != null)
            {
                _hurtBox.SetUp(this);
            }
        }

        // Controller
        if (_controller == null)
        {
            _controller = GetComponent<MonsterController>();
        }

        if (_controller != null)
        {
            _controller.SetUp(this);
        }
                  
    }

    public void TakeDamage(CarochitoBattler attacker, SkillBase skill)
    {
        DamageCalculator damageCalculator = new();
        float damage = damageCalculator.CaculateDamage(attacker.Carochito, Carochito, skill);

        Carochito.CurrentHealth -= (int)damage;

        string alert = Carochito.Name + " recebeu " + damage + " de dano";
        AlertManager.instance.AddAlert(Carochito.Base.Sprite, alert);

        if (_isEnemy == true)
        {
            if (_enemyHealthBar != null)
            {
                _enemyHealthBar.SetHealth(Carochito.CurrentHealth);
            }
        }
        else
        {
            if (_allyHealthBar != null)
            {
                _allyHealthBar.SetHealth(Carochito.CurrentHealth);
            }
        }

        if (Carochito.CurrentHealth <= 0)
        {
            Die(attacker);
        }
    }

    public void Heal(int heal)
    {
        if (Carochito.IsAlive)
        {
            Carochito.CurrentHealth += (int)heal;

            if (_isEnemy == true)
            {
                if (_enemyHealthBar != null)
                {
                    _enemyHealthBar.SetHealth(Carochito.CurrentHealth);
                }
            }
            else
            {
                if (_allyHealthBar != null)
                {
                    _allyHealthBar.SetHealth(Carochito.CurrentHealth);
                }
            }
        } 
    }

    public void Eat()
    {
        
    }

    private Action onDeath;

    public void SetUp(Action deathCallback)
    {
        onDeath = deathCallback;
    }

    public void Die(CarochitoBattler attacker)
    {
        Carochito.CurrentHealth = 0;
        Carochito.IsAlive = false;

        string alert = Carochito.Name + " foi derrotado!";
        AlertManager.instance.AddAlert(Carochito.Base.Sprite, alert);

        _controller.animator.SetTrigger("die");

        if (_isEnemy == true)
        {
            alert = attacker.Carochito.Name + " recebeu " + 25 + " de Exp!";
            AlertManager.instance.AddAlert(Carochito.Base.Sprite, alert);

            attacker.Carochito.GetExp(25);
        }
        else
        {
            _owner.SwapToTrainer();
        }
        
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

        // Destroy this Carochito
        Destroy(gameObject);

        onDeath?.Invoke();
    }

    public void Capture()
    {
        if (_isEnemy == true)
        {
            _isGettingCaptured = true;

            StartCoroutine(CaptureRoutine());
        }
    }

    private IEnumerator CaptureRoutine()
    {
        _controller.animator.SetTrigger("takeDamage");
        _controller.stateMachine.ChangeState(_controller.enemyCaptureState);

        yield return null;

        AnimatorStateInfo stateInfo = _controller.animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(stateInfo.length);

        // Instanciate Disk
        GameObject discObject = Instantiate( _diskPrefab, _capturePoint.position, _capturePoint.rotation);

        if (discObject.TryGetComponent<DiscCapture>(out var disc))
        {
            disc.SetUp(Carochito, gameObject);
        }

        // Destroy this Carochito
        Destroy(gameObject);
    }
}
