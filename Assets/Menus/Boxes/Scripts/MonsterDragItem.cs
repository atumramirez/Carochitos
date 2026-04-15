using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;

    [Header("Monster")]
    public Carochito carochito;
    public RectTransform icon;
    public Image smallImageBackground;
    public Image bigImageBackground;

    [Header("Is on Party")]
    public bool isOnParty;

    [Header("Small Icon")]
    public GameObject smallIcon;
    public Image smallIconImage;
    public TextMeshProUGUI smallIconName;

    [Header("Big Icon")]
    public GameObject bigIcon;
    public Image bigIconImage;
    public TextMeshProUGUI bigIconName;
    public TextMeshProUGUI level;
    public Slider healthBar;

    [Header("Boxes Menu")]
    public BoxesMenu boxesMenu;


    private void Start()
    {
        boxesMenu = FindFirstObjectByType<BoxesMenu>();
    }

    public void Setup(Carochito car, bool isParty)
    {
        carochito = car;

        if (carochito.Base.Sprite != null)
        {
            smallIconImage.sprite = carochito.Base.Sprite;
            bigIconImage.sprite = carochito.Base.Sprite;
        }

        smallIconName.text = carochito.Name;
        bigIconName.text = carochito.Name;

        level.text = "LV. " + carochito.Level;

        healthBar.maxValue = carochito.Base.MaxHealth;
        healthBar.value = carochito.CurrentHealth;

        UpdateIcon(isParty);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        UpdateIcon(false);

        smallImageBackground.raycastTarget = false;
        bigImageBackground.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);

        if (parentAfterDrag.parent == boxesMenu.partyContainer.transform)
        {
            UpdateIcon(true);
        }
        else
        {
            UpdateIcon(false);
        }

        // Quando um Carochito é movido guarda autumaticamente na lsitas as mudanças
        if (boxesMenu != null)
        {
            boxesMenu.Organize(boxesMenu.partyContainer.transform);
            boxesMenu.SaveList(boxesMenu.partyContainer.transform, Party.Instance.carochitos);

            boxesMenu.SaveList(boxesMenu.boxesContainer.transform, Boxes.Instance.Box1);
        }
    }

    public void UpdateIcon(bool isParty)
    { 
        bigImageBackground.raycastTarget = isParty;
        bigIcon.SetActive(isParty);

        smallImageBackground.raycastTarget = !isParty;
        smallIcon.SetActive(!isParty);

        if (isParty == false)
        {
            icon.sizeDelta = new Vector2(180f, 180f);
        }
    }
}
