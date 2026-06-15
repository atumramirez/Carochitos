using UnityEngine;
using UnityEngine.UI;

public class CarochitoSelectionSheet : MonoBehaviour
{
    public Image CarochitoImage;

    [Header("Button Sprites")]
    public Image background;
    public Sprite selectedButton;
    public Sprite desselectedButton;

    public void UpdateSheet(Carochito carochito)
    {
        CarochitoImage.sprite = carochito.Base.Sprite;
    }

    public void Selected(bool selected)
    {
        if (selected == false)
        {
            background.sprite = selectedButton;
        }
        else
        {
            background.sprite = desselectedButton;
        }
    }
}
