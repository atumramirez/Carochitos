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
        if (MenuToOpen.Count != 0 || MenuToOpen != null)
        {
            foreach (GameObject go in MenuToOpen)
            {
                go.SetActive(true);
            }
        }

        if (MenuToClose.Count != 0 || MenuToClose != null)
        {
            foreach (GameObject go in MenuToClose)
            {
                go.SetActive(false);
            }
        }
    }
}
