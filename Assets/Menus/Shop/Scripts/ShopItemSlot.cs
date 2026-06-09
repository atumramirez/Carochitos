using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public TextMeshProUGUI cost;
    public Button button;

    public ItemInInventory _item;

    public void Setup(ItemInInventory item)
    {
        _item = item;
        icon.sprite = item.Item._sprite;
        cost.text = _item.Item._cost.ToString() + "$";
    }
}
