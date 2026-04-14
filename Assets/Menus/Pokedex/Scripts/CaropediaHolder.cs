using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaropediaHolder : MonoBehaviour
{
    [Header("Icon")]
    public Image _icon;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _description;

    [Header("Menu")]
    public GameObject _panel;

    public CaropediaSlot _currentSelectSlot;
    public void RefreshMenu()
    {
        if (_currentSelectSlot != null)
        {
            _panel.SetActive(true);
            _icon.sprite = _currentSelectSlot.carochito.Base.Sprite;
            _name.text = _currentSelectSlot.carochito.Base.Name;
            _description.text = _currentSelectSlot.carochito.Base.Description;
        }
        else
        {
            _panel.SetActive(false);
        }
    }
}
