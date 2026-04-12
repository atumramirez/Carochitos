using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI")]
    public Slider healthBar;
    public TextMeshProUGUI healthNumber;

    public virtual void SetMaxHealth(Carochito carochito)
    {
        healthBar.maxValue = carochito.Base.MaxHealth;
        healthBar.value = carochito.Base.MaxHealth;

        healthNumber.text = healthBar.value.ToString();
    }

    public virtual void SetHealth(int health)
    {
        healthBar.value = health;
        healthNumber.text = healthBar.value.ToString();
    }
}
