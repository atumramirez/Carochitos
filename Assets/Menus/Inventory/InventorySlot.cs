using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public TextMeshProUGUI count;
    public Button button;

    public ItemInInventory _item;

    public void Setup(ItemInInventory item)
    {
        _item = item;
        count.text = "x" + _item.Count.ToString();
    }
}
