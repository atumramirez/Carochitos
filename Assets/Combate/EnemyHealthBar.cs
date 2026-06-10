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
        healthBar.maxValue = carochito.Base.MaxHealth;
        healthBar.value = carochito.Base.MaxHealth;
    }

    public override void SetHealth(int health)
    {
        healthBar.value = health;

        if (healthBar.value == healthBar.maxValue)
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
        Debug.Log("Appear");
        _health.SetActive(true);
    }

    public void HideSlider()
    {
        Debug.Log("Disappear");
        _health.SetActive(false);
    }

    public void SetName(string name)
    {
        _name.text = name;
    }
}
