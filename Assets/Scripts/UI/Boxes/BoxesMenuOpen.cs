using UnityEngine;

public class BoxesMenuOpen : MonoBehaviour
{
    public bool isOpened = false;
    public GameObject menu;

    public PartyHolder partyHolderBoxes;

    private void Start()
    {
        ActivateMenu();
    }
    public void ActivateMenu()
    {
        menu.SetActive(isOpened);

        partyHolderBoxes.PopulateFromList();
        partyHolderBoxes.Organize();

        isOpened = !isOpened;
    }
}
