using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> _item = new();

    public void AddItem(Item item, int count)
    {
        ItemSlot existingSlot = _item.Find(slot => slot._item == item);

        if (existingSlot != null)
        {
            existingSlot._count += count;
        }
        else
        {
            _item.Add(new ItemSlot { _item = item, _count = count });
        }
    }
}
