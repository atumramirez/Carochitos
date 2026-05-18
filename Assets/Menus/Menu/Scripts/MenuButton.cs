using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [Header("Menus")]
    public List<GameObject> MenuToOpen;
    public List<GameObject> MenuToClose;

    [Header("Audio")]
    public AudioClip MenuSound1;
    public AudioClip MenuSound2;

    Button button;

    private void Start()
    {
        MenuSound1 = SoundHolder.Instance.MenuInteract;
        MenuSound2 = SoundHolder.Instance.MenuInteract;
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
                    SoundManager.instance.PlayClip(MenuSound1, transform, 0.75f);
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
                    SoundManager.instance.PlayClip(MenuSound2, transform, 0.75f);
                    go.SetActive(false);
                }
            }
        }
    }
}
