using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    [Header("Inventory")]
    public Inventory inventory;
    ItemBase currentSelectedItem;

    [Header("Pocket")]
    Pocket currentPocket = Pocket.Medicine;
    private Pocket[] allPockets;
    public TextMeshProUGUI pocketName;

    [Header("Item Container")]
    public GameObject itemContainer;
    public GameObject itemPrefab;

    [Header("Button")]
    public Button useButton;

    [Header("Item Info")]
    public GameObject itemInfoPanel;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    private void Awake()
    {
        allPockets = (Pocket[])System.Enum.GetValues(typeof(Pocket));
    }

    private void Start()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        currentSelectedItem = null;
        pocketName.text = currentPocket.ToString();

        // Destruir a Children
        foreach (Transform child in itemContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemInInventory item in inventory.inventory)
        {
            if (item.Item._pocket == currentPocket)
            {
                GameObject obj = Instantiate(itemPrefab, itemContainer.transform);

                if (obj.TryGetComponent<InventorySlot>(out var ui))
                {
                    ui.Setup(item);

                    if (ui.button != null)
                    {
                        ui.button.onClick.AddListener(() => SelectItem(item));
                    }
                }
            }
        }

        UpdateMenu();
    }

    public void SelectItem(ItemInInventory item)
    {
        currentSelectedItem = item.Item;

        UpdateMenu();
    }

    public void UpdateMenu()
    {
        if (currentSelectedItem != null)
        {
            itemInfoPanel.SetActive(true);

            useButton.interactable = true;

            itemName.text = currentSelectedItem._name;
            itemDescription.text = currentSelectedItem._description;
        }
        else 
        {
            useButton.interactable = false;
            itemInfoPanel.SetActive(false);
        }
    }

    public void NextPocket()
    {
        int index = System.Array.IndexOf(allPockets, currentPocket);

        index = (index + 1) % allPockets.Length;

        currentPocket = allPockets[index];

        OpenMenu();
    }

    public void PreviousPocket()
    {
        int index = System.Array.IndexOf(allPockets, currentPocket);

        index--;

        if (index < 0)
            index = allPockets.Length - 1;

        currentPocket = allPockets[index];

        OpenMenu();
    }
}
