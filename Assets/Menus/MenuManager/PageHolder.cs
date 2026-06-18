using System.Collections.Generic;
using UnityEngine;

public class PageHolder : MonoBehaviour
{
    public List<Page> menuList;
    public int _startMenu = 0;
    public Page _currentMenu;

    public void Start()
    {
        SetUp();
    }

    public virtual void SetUp()
    {
        OpenPage(_startMenu);
    }

    public virtual void OpenPage(int pageToOpen = 0)
    {
        Page openPage = menuList[pageToOpen];

        foreach (var menu in menuList)
        {
            if (menu != openPage)
            {
                menu.CloseMenu();
            }
            else
            {
                menu.OpenMenu();
                _currentMenu = menu;
            }
        }
    }

    public virtual void ClosePage()
    {
        foreach (var menu in menuList)
        {
            menu.CloseMenu();
        }
    }

    public virtual void OpenMenu()
    {
        
    }
}
