using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterSlot : MonoBehaviour, IDropHandler
{
    [Header("Boxes Menu")]
    public BoxesMenu boxesMenu;

    [Header("Containers")]
    public GameObject partyContainer;
    public GameObject boxesContainer;

    public virtual void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        MonsterDragItem draggableItem = dropped.GetComponent<MonsterDragItem>();
        Transform originalParent = draggableItem.parentAfterDrag;

        /*
        if (transform.childCount == 0 && organizer.GetItemCount() == 0)  
        {
            Debug.Log("Este é o ultimo carochito! Tens que ter pelo menos 1 na equipa! ");
            draggableItem.parentAfterDrag = originalParent;
            return;
        }
        */

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
