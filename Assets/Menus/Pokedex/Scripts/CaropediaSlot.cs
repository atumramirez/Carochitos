using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CaropediaSlot : MonoBehaviour
{
    public Carochito carochito;
    public Button button;

    [Header("Info")]
    public Image _image;
    public TextMeshProUGUI _name;

    public CaropediaHolder caropediaHolder;

    private void Start()
    {
        button = GetComponent<Button>();
        caropediaHolder = GetComponentInParent<CaropediaHolder>();

        button.onClick.AddListener(SelectCarochito);
    }

    public void SelectCarochito()
    {
        caropediaHolder._currentSelectSlot = this;
        caropediaHolder.RefreshMenu();
    }

    public void Refresh()
    {
        _image.sprite = carochito.Base.Sprite;
        _name.text = carochito.Name;
    }
}
