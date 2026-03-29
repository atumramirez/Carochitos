using UnityEngine;

public class GiveItem : MonoBehaviour
{
    public Item item;
    public int amount;

    public void GiveItem2() 
    {
        Inventory.Instance.AddItem(item, amount);
        Debug.Log("Adicionar Item");
    }
}
