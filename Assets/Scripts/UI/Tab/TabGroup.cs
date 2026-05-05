using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{    
    public List<TabButton> tabButtons;

    [Header("Sprites")]
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    [HideInInspector] public TabButton selectedTab;

    [Header("Page to Open")]
    public List<GameObject> objectsToSwap;

    public void Subscribe(TabButton button)
    {
        tabButtons ??= new List<TabButton>();
        objectsToSwap ??= new List<GameObject>();

        if (!tabButtons.Contains(button))
        {
            tabButtons.Add(button);
        }
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();

        if (selectedTab == null || button != selectedTab)
        {
            if (tabHover != null)
            {
                button.background.sprite = tabHover;
            }
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

        if (tabActive != null)
        {
            button.background.sprite = tabActive;
        }

        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (objectsToSwap[i] != null)
            {
                if (i == index)
                {
                    objectsToSwap[i].SetActive(true);
                }

                else
                {
                    if (objectsToSwap[i].TryGetComponent<TabGroup>(out var comp))
                    {
                        for (int j = 0; j < comp.objectsToSwap.Count; j++)
                        {
                            if (comp.objectsToSwap[j] != null)
                            {
                                comp.objectsToSwap[j].SetActive(false);
                            } 
                        }
                    }

                    objectsToSwap[i].SetActive(false);
                }
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

            if (button.background == null)
            {
                button.background= button.GetComponent<Image>();
            }

            if (tabIdle != null)
            {
                button.background.sprite = tabIdle;
            }

        }
    }
}
