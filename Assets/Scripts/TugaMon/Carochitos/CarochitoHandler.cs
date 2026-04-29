using UnityEngine;

public class CarochitoHandler : MonoBehaviour
{
    public Carochito carochito;

    [Header("UI")]
    public HealthBar healthBar;


    private void Start()
    {
        healthBar = GameObject.Find("HealthSlider").GetComponent<HealthBar>();
        carochito.CurrentHealth = carochito.Base.MaxHealth;
        healthBar.SetMaxHealth(carochito);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(1);
        }
    }

    public void TakeDamage(CarochitoHandler owner, SkillBase power)
    {
        int damage = owner.carochito.Attack + power.Power; 

        carochito.CurrentHealth -= damage;
        healthBar.SetHealth(carochito.CurrentHealth);
    }

    // Debug
    public void Damage(int damage)
    {
        carochito.CurrentHealth -= damage;
        healthBar.SetHealth(carochito.CurrentHealth);
    }
}
