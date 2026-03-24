using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public bool isOpened = false;

    public GameObject _playerMenu;
    
    public CarochitoPartyHolder _carochitoHolder;

    private void Start()
    {
        ActivateMenu();
    }

    public void ActivateMenu()
    {
        _carochitoHolder.RefreshParty();

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
        _playerMenu.SetActive(isOpened);
    }

    public void CloseMenu() 
    {
        isOpened = false;
        _playerMenu.SetActive(isOpened);
    }
}
