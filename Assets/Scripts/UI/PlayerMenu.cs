using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    [Header("All Menus")]
    public List<GameObject> allPanels;
    public List<GameObject> selectedPanels;

    [Header("Information")]
    public bool isOpened = true;
    public GameObject currentOpenMenu;

    [Header("Menus")]
    public GameObject _playerMenu; 
    public GameObject _inGameMenu;
    public GameObject _dialogueMenu;

    private void Start()
    {
        foreach (GameObject panel in allPanels)
        {
            panel.SetActive(false);
        }

        foreach (GameObject selpanel in selectedPanels)
        {
            selpanel.SetActive(true);
        }

        ActivateMenu();
    }

    public void ActivateMenu()
    {
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
