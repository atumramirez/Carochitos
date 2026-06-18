using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public ItemBase Item;
    public int Count;
    public Animator Animator;

    public void OpenBox()
    {
        Animator.SetTrigger("open");
        GiveItem(Item, Count);
    }

    public void GiveItem(ItemBase _item, int _count)
    {
        ActionManager.Instance.inventory.AddItem(_item, _count);

        string alert = "Recebeste " + _count + " " + _item._name;
        AlertManager.instance.AddAlert(Item._sprite, alert);
    }
}
