using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : PageHolder
{
    [Header("PageHolders")]
    public List<PageHolder> _menus;
    public PageHolder _currentPlayerMenu;

    public override void SetUp()
    {
        
    }

    public override void OpenPage(int pageToOpen)
    {
        PageHolder openPage = _menus[pageToOpen];

        foreach (var menu in _menus)
        {
            if (menu != openPage)
            {
                menu.ClosePage();
                //menu.gameObject.SetActive(false);
            }
            else
            {
                //menu.gameObject.SetActive(true);
                menu.OpenPage(0);
                _currentPlayerMenu = menu;
            }
        }
    }

    public override void ClosePage()
    {
        foreach (var menu in _menus)
        {
            menu.ClosePage();
        }
    }
}
