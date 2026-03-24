using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    public TabButton selectedTab;

    public List<GameObject> objectsToSwap;

    public void Subscribe(TabButton button)
    {
        tabButtons ??= new List<TabButton>();
        objectsToSwap ??= new List<GameObject>();

        if (!tabButtons.Contains(button))
        {
            tabButtons.Add(button);
        }

        if (button.tabContainer != null && !objectsToSwap.Contains(button.tabContainer))
        {
            objectsToSwap.Add(button.tabContainer);
        }
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();

        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabClick(TabButton button)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;
        selectedTab.Select();

        ResetTabs();
        button.background.sprite = tabActive;

        for (int i = 0; i < tabButtons.Count; i++)
        {
            bool isSelected = tabButtons[i] == button;

            if (tabButtons[i].tabContainer != null)
            {
                tabButtons[i].tabContainer.SetActive(isSelected);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab)
            {
                continue;
            }

            button.background.sprite = tabIdle;
        }
    }
}
