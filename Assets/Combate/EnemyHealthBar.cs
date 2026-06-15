using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : HealthBar
{
    public Camera _camera;

    [Header("UI")]
    public GameObject _health;
    public TextMeshProUGUI _name;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = _camera.transform.rotation;
    }

    public override void SetMaxHealth(Carochito carochito)
    {
        healthBar.maxValue = carochito.Health;
        healthBar.value = carochito.Health;
    }

    public override void SetHealth(int health)
    {
        healthBar.value = health;

        if (healthBar.value == healthBar.maxValue || healthBar.value == 0)
        {
            HideSlider();
        }
        else
        {
            ShowSlider();
        }
    }

    public void ShowSlider()
    {
        _health.SetActive(true);
    }

    public void HideSlider()
    {
        _health.SetActive(false);
    }

    public void SetName(string name)
    {
        _name.text = name;
    }
}
