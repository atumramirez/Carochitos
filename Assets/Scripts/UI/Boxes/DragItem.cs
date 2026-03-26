using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public Image image;
    public Image Sprite;
    public Carochito carochito;

    private void Start()
    {
        image = GetComponent<Image>();
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

        PartyHolder.Instance.PopulateToList();
        PartyHolder.Instance.Organize();

        BoxesHolder.Instance.PopulateToList();
    }
}
