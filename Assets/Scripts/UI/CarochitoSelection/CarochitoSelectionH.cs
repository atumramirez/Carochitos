using UnityEngine;
using UnityEngine.UI;

public class CarochitoSelectionSheet : MonoBehaviour
{
    public Image CarochitoImage;

    public void UpdateSheet(Carochito carochito)
    {
        CarochitoImage.sprite = carochito.Base.Sprite;
    }
}
