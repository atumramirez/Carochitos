using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;

    [Header("Monster")]
    public Carochito carochito;
    public Image image;
    public bool isOnParty;

    [Header("Small Icon")]
    public Image smallIconImage;
    
    [Header("Big Icon")]
    public Image bigIconImage;
    public TextMeshProUGUI level;
    public Slider healthBar;

    public PartyHolder organizer;
    public BoxesHolder boxesHolder;

    private void Start()
    {
        organizer = FindFirstObjectByType<PartyHolder>();
        boxesHolder = FindFirstObjectByType<BoxesHolder>();
    }

    public void Setup(Carochito car)
    {
        carochito = car;
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
