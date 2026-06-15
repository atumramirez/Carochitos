using System.Collections.Generic;
using UnityEngine;

public class BookMenu : MonoBehaviour
{
    public List<PageHolder> pageHolders;
    public int StartPage = 0;
    public PageHolder _currentPageHolder;

    private void Start()
    {
        OpenBook(StartPage);
    }

    public void OpenBook(int startPage)
    {
        PageHolder openPage = pageHolders[startPage];

        foreach (var menu in pageHolders)
        {
            if (menu != openPage)
            {
                menu.ClosePage();
            }
            else
            {
                menu.OpenPage(0);
                _currentPageHolder = menu;
            }
        }
    }
}
