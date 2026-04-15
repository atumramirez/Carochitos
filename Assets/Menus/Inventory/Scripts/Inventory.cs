using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInInventory> inventory = new();

    public void AddItem(ItemBase item, int count)
    {
        ItemInInventory existingSlot = inventory.Find(slot => slot.Item == item);

        if (existingSlot != null)
        {
            existingSlot.Count += count;
        }
        else
        {
            inventory.Add(new ItemInInventory { Item = item, Count = count });
        }
    }
}
