using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarochitoPartySheet: MonoBehaviour
{
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _lv;

    public int _maxHealth;
    public int _currentHealth;

    public Image _sprite;

    public Slider _healthSlider;

    public void UpdateSheet(Carochito carochito)
    {
        _name.text = carochito.Base.Name; 
        _lv.text = "Lv. " + carochito.Level;

        _maxHealth = carochito.Base.MaxHealth;
        _currentHealth = carochito.CurrentHealth;

        _sprite.sprite = carochito.Base._sprite;

        UpdateSlider(carochito.Base.MaxHealth, carochito.CurrentHealth);
    }

    public void UpdateSlider(int max, int current)
    {
        _healthSlider.maxValue = max;
        _healthSlider.value = current;
    }
}
