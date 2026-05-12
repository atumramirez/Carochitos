using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [Header("Menus")]
    public List<GameObject> MenuToOpen;
    public List<GameObject> MenuToClose;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HideMenu);
    }

    public void HideMenu()
    {
        if (MenuToOpen != null)
        {
            if (MenuToOpen.Count != 0)
            {
                foreach (GameObject go in MenuToOpen)
                {
                    go.SetActive(true);
                }
            }
        }

        if (MenuToClose != null)
        {
            if (MenuToClose.Count != 0)
            {
                foreach (GameObject go in MenuToClose)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}
