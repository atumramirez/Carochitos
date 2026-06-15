using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI")]
    public Slider healthBar;
    public TextMeshProUGUI healthNumber;

    public virtual void SetMaxHealth(Carochito carochito)
    {
        healthBar.maxValue = carochito.Health;
        healthBar.value = carochito.Health;

        if (healthNumber != null)
        {
            healthNumber.text = healthBar.value.ToString();
        }
    }

    public virtual void SetHealth(int health)
    {
        healthBar.value = health;

        if (healthNumber != null)
        {
            healthNumber.text = "" + health;
        }
    }
}
