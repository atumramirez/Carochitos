using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertCommand : MonoBehaviour
{
    public Image _alertIcon;
    public TextMeshProUGUI _alertText;

    public void SetUp(Carochito carochito, string text)
    {
        _alertText.text = text;
        _alertIcon.sprite = carochito.Base.Sprite;

        Destroy(gameObject, 3f);
    }
}