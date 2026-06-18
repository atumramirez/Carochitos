using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Currency")]
    public int Currency = 0;

    [Header("Items")]
    public List<ItemInInventory> inventory = new();

    [Header("Berliner")]
    public List<ItemInInventory> allBerliner = new();
    public int currentBerlinerIndex;
    public ItemInInventory currentBerliner;

    [Header("Disk")]
    [SerializeField] ItemInInventory disk;

    [Header("HuD")]
    public CarochitoSelectionMenu berlinerMenu;

    [Header("Menu")]
    public InventoryMenu inventoryMenu;

    public void Start()
    {
        CheckBerliner();
    }

    public void CheckBerliner()
    {
        foreach (ItemInInventory item in inventory)
        {
            if (item.Item is Berliner)
            {
                allBerliner.Add(item);
            }
        }

        if (allBerliner.Count > 0)
        {
            currentBerlinerIndex = 0;
            currentBerliner = allBerliner[currentBerlinerIndex];
        }
    }
    public void NextBerliner()
    {
        if (allBerliner.Count == 0) return;

        currentBerlinerIndex = (currentBerlinerIndex + 1) % allBerliner.Count;
        currentBerliner = allBerliner[currentBerlinerIndex];
    }

    public void PreviousBerliner()
    {
        if (allBerliner.Count == 0) return;

        currentBerlinerIndex = (currentBerlinerIndex - 1 + allBerliner.Count) % allBerliner.Count;
        currentBerliner = allBerliner[currentBerlinerIndex];
    }


    public void AddBerliner(ItemBase item, int count)
    {
        ItemInInventory newBerliner = allBerliner.Find(slot => slot.Item == item);

        if (newBerliner != null)
        {
            newBerliner.Count += count;
        }
        else
        {
            ItemInInventory newItem = new() { Item = item, Count = count };
            allBerliner.Add(newItem);
        }

        currentBerliner = allBerliner[0];
    }


    public void AddItem(ItemBase item, int count = 1)
    {
        ItemInInventory existingSlot = inventory.Find(slot => slot.Item == item);

        if (existingSlot != null)
        {
            existingSlot.Count += count;
        }
        else
        {
            ItemInInventory newItem = new() { Item = item, Count = count };
            inventory.Add(newItem);
        }



        if (existingSlot.Item is Berliner berliner)
        {
            AddBerliner(berliner, count);
        }
    }

    public bool CanRemoveItem(ItemBase item)
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

    public void RemoveItem(ItemBase item, int count)
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

            /*
            if (existingSlot.Item is Berliner berliner)
            {
                ItemInInventory removeBerliner = allBerliner.Find(slot => slot.Item == item);
                removeBerliner.Count -= count;

                if (removeBerliner.Count <= 0)
                {
                    allBerliner.Remove(removeBerliner);
                }
            }
            */
        }
        else
        {
            Debug.Log("Não há nada para retirar");
        }
    }

    public void BuyItem(ItemBase item, int count, int cost)
    {
        Currency -= cost;
        AddItem(item, count);
    }
}
