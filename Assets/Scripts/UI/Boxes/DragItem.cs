using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public Image image;
    public Carochito carochito;

    public PartyHolder organizer;
    public BoxesHolder boxesHolder;

    private void Start()
    {
        image = GetComponent<Image>();
        organizer = FindFirstObjectByType<PartyHolder>();
        boxesHolder = FindFirstObjectByType<BoxesHolder>();
    }

    public void Refresh(Carochito car)
    {
        carochito = car;
        // image.sprite = carochito.Base._sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;

        if (organizer != null)
        {
            organizer.Organize();
            organizer.PopulateToList();
            boxesHolder.PopulateToList();
        }
    }
}
