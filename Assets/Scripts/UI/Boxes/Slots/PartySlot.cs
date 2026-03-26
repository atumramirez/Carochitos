using UnityEngine;
using UnityEngine.EventSystems;

public class PartySlot : InventorySlot
{
    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragItem draggableItem = dropped.GetComponent<DragItem>();
        Transform originalParent = draggableItem.parentAfterDrag;

        if (transform.childCount == 0)
        {
            draggableItem.parentAfterDrag = transform;
        }
        else
        {
            Transform itemInSlot = transform.GetChild(0);

            itemInSlot.SetParent(originalParent);
            itemInSlot.SetAsLastSibling();

            draggableItem.parentAfterDrag = transform;
        }
    }
}
