using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public bool isOpened = true;

    public GameObject _playerMenu; 
    public GameObject _inGameMenu;

    //public CarochitoPartyHolder _carochitoHolder;

    private void Start()
    {
        ActivateMenu();
    }

    public void ActivateMenu()
    {
        //_carochitoHolder.RefreshParty();

        if (!isOpened)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    public void OpenMenu()
    {
        isOpened = true;
        Cursor.lockState = CursorLockMode.None;
        _playerMenu.SetActive(isOpened);
        _inGameMenu.SetActive(!isOpened);
    }

    public void CloseMenu() 
    {
        isOpened = false;
        Cursor.lockState = CursorLockMode.Locked;
        _playerMenu.SetActive(isOpened);
        _inGameMenu.SetActive(!isOpened);
    }
}
