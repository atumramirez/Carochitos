using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public Image image;
    public Image Sprite;
    public Carochito carochito;

    public PartyHolder organizer;
    public BoxesHolder partyHolder;

    private void Start()
    {
        image = GetComponent<Image>();
        organizer = FindFirstObjectByType<PartyHolder>();
        partyHolder = FindFirstObjectByType<BoxesHolder>();
    }

    public void Refresh(Carochito car)
    {
        carochito = car;
        Sprite.sprite = car.Base._sprite;
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

        organizer.PopulateToList();
        partyHolder.PopulateToList();


        if (organizer != null)
        {
            organizer.Organize();
        }
    }
}
