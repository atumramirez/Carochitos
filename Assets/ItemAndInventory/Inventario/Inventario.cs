using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour 
{
    public static Inventory Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<InventoryItem> _playerInventory;

    // Quando adicionar o sistema de Save, fazer o sistema ler os dados que estão no save

    public void AddItem(Item item, int amount)
    {
        InventoryItem slot = new(item, amount);

        if (_playerInventory.Count == 0)
        {
            _playerInventory.Add(slot);
        }
        else
        {
            bool isThereItem = false;

            foreach (InventoryItem i in _playerInventory)
            {
                if (slot.Item == i.Item)
                {
                    i.Amount += slot.Amount;
                    isThereItem = true;
                    break;
                }
            }

            if (isThereItem == false)
            {
                _playerInventory.Add(slot);
            }
        }
    }
}


