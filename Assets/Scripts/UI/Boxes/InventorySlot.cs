using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public PartyHolderBoxes organizer;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragItem draggableItem = dropped.GetComponent<DragItem>();
        Transform originalParent = draggableItem.parentAfterDrag;

        if (transform.childCount == 0 && organizer.GetItemCount() == 0)  
        {
            Debug.Log("Este é o ultimo carochito! Tens que ter pelo menos um na equipa! ");
            draggableItem.parentAfterDrag = originalParent;
            return;
        }

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
