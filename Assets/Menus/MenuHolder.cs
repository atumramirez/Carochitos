using UnityEngine;

public class MenuHolder : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject menu;

    public void OpenMenu()
    {
        if (isOpen == true)
        {
            isOpen = false;
        }
        else
        {
            isOpen = true;
        }
        
        menu.SetActive(isOpen);
    }
}
