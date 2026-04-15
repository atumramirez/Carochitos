using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterSlot : MonoBehaviour, IDropHandler
{
    [Header("Boxes Menu")]
    public BoxesMenu boxesMenu;

    public virtual void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropaste aqui uma cena");

        GameObject dropped = eventData.pointerDrag;

        MonsterDragItem draggableItem = dropped.GetComponent<MonsterDragItem>();
        Transform originalParent = draggableItem.parentAfterDrag;
        
        if (transform.childCount == 0 && boxesMenu.GetItemCount(boxesMenu.partyContainer.transform) == 0)  
        {
            Debug.Log("Este é o ultimo Carochito! Tens que ter pelo menos 1 na equipa! ");

            draggableItem.parentAfterDrag = originalParent;
            return;
        }
        
        if (transform.childCount == 0)
        {
            Debug.Log("Nice!");
            draggableItem.parentAfterDrag = transform;
        }
        else
        {
            Debug.Log("Não havia espaço, voltou para o sitio original");

            Transform itemInSlot = transform.GetChild(0);

            itemInSlot.SetParent(originalParent);
            itemInSlot.SetAsLastSibling();

            draggableItem.parentAfterDrag = transform;
        }
    }
}
