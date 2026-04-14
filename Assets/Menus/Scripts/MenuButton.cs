using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [Header("Menus")]
    public GameObject MenuToOpen;
    public GameObject MenuToClose;

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
            MenuToOpen.SetActive(true);
        }

        if (MenuToClose != null)
        {
            MenuToClose.SetActive(false);
        }
    }
}
