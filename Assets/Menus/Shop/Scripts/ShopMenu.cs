using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [Header("Inventory")]
    public Inventory inventory;
    ItemBase currentSelectedItem;

    [Header("Shop")]
    public GameObject shopMenu;
    public List<ItemInInventory> itemsOnShop;

    [Header("Item Container")]
    public GameObject itemContainer;
    public GameObject itemPrefab;

    [Header("Button")]
    public Button buyButton;

    [Header("Item Info")]
    public GameObject itemInfoPanel;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    [Header("Buy Panel")]
    public GameObject confirmShopMenu;
    public Button moreOne;
    public Button lessOne;
    public Button confirmBuy;
    public int amountItens;
    public TextMeshProUGUI amount;
    public int price;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI money;

    private void Start()
    {

        OpenMenu();
    }

    public void OpenMenu()
    {

        confirmShopMenu.SetActive(false);

        currentSelectedItem = null;

        foreach (Transform child in itemContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemInInventory item in itemsOnShop)
        {
                GameObject obj = Instantiate(itemPrefab, itemContainer.transform);

                if (obj.TryGetComponent<ShopItemSlot>(out var ui))
                {
                    ui.Setup(item);

                    if (ui.button != null)
                    {
                        ui.button.onClick.AddListener(() => SelectItem(item));
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

            buyButton.interactable = true;

            itemName.text = currentSelectedItem._name;
            itemDescription.text = currentSelectedItem._description;
        }
        else
        {
            buyButton.interactable = false;
            itemInfoPanel.SetActive(false);
        }
    }

    public void CloseMenu()
    {
        shopMenu.SetActive(true);
        confirmShopMenu.SetActive(false);
    }

    public void OpenShopMenu()
    {
        shopMenu.SetActive(false);
        confirmShopMenu.SetActive(true);

        money.text = "Money: " + inventory.Currency.ToString();

        amountItens = 1;
        amount.text = amountItens.ToString();

        price = amountItens * currentSelectedItem.cost;
        priceText.text = "Price: " + price.ToString();

        UpdateShopMenu();
    }

    public void MoreItens(int number)
    {
        amountItens += number;
        amount.text = amountItens.ToString();

        price = amountItens * currentSelectedItem.cost;
        priceText.text = "Price: " + price.ToString();

        UpdateShopMenu();
    }

    public void UpdateShopMenu()
    {
        if ((amountItens + 1) * currentSelectedItem.cost > inventory.Currency)
        {
            moreOne.interactable = false;
        }
        else
        {
            moreOne.interactable = true;
        }

        if (amountItens == 1)
        {
            lessOne.interactable = false;
        }
        else
        {
            lessOne.interactable = true;
        }

        if (currentSelectedItem.cost > inventory.Currency)
        {
            confirmBuy.interactable = false;
        }
        else
        {
            confirmBuy.interactable = true;
        }

        money.text = "Money: " + inventory.Currency;
    }

    public void Buy()
    {
        inventory.BuyItem(currentSelectedItem, amountItens, price);

        UpdateShopMenu();

        CloseMenu();
    }
}
