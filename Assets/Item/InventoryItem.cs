using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    [SerializeField] Item _item;
    [SerializeField] int _amount;

    public Item Item { get { return _item; } }
    public int Amount { get { return _amount; } set { _amount = value; } }

    public InventoryItem(Item item, int amount)
    {
        _item = item;
        _amount = amount;
    }
}