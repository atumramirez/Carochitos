using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Currency")]
    public int Currency = 0;

    [Header("Items")]
    public List<ItemInInventory> inventory = new();

    public void AddItem(ItemBase item, int count) // Adicionar Item
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

    public bool CanRemoveItem(ItemBase item) // Check para remover Item do Inventario
    {
        ItemInInventory existingSlot = inventory.Find(slot => slot.Item == item);

        if (existingSlot != null)
        {
            if (existingSlot.Count > 0)
            {
                Debug.Log("Consigo remover Item");
                return true;
            }
            else
            {
                Debug.Log("Não consigo remover Item, o Item tem menos de 0 em quantidade");
                return false;
            }
        }
        else
        {
            Debug.Log("Não consigo remover Item, não existe Item");
            return false;
        }
    }

    public void RemoveItem(ItemBase item, int count) // Remover Item do Inventario
    {

        ItemInInventory existingSlot = inventory.Find(slot => slot.Item == item);

        if (existingSlot != null)
        {
            Debug.Log("Retiramos: " + count + " do Item: " + item.name);
            existingSlot.Count -= count;

            if (existingSlot.Count <= 0)
            {
                Debug.Log("O Item: " + item.name + " foi removido do Inventario");
                inventory.Remove(existingSlot);
            }
        }
        else
        {
            Debug.Log("Não há nada para retirar");
        }
    }

    public void BuyItem(ItemBase item, int count, int cost) // Comprar Item
    {
        Currency -= cost;
        AddItem(item, count);
    }


}
