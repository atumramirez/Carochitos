using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : HealthBar
{
    public Camera _camera;

    [Header("UI")]
    public TextMeshProUGUI _level;
    public TextMeshProUGUI _name;

    public override void SetMaxHealth(Carochito carochito)
    {
        healthBar.maxValue = carochito.Base.MaxHealth;
        healthBar.value = carochito.Base.MaxHealth;

        _level.text = "Lv. " + carochito.Level;
        _name.text = carochito.Name;
    }

    public override void SetHealth(int health)
    {
        healthBar.value = health;
    }
}
