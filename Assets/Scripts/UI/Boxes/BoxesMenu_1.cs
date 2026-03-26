using UnityEngine;

public class BoxesMenu : MonoBehaviour
{
    public bool isOpened = false;
    public GameObject menu;

    public PartyHolder partyHolder;
    public BoxesHolder boxesHolder;

    private void Start()
    {
        ActivateMenu();
    }
    public void ActivateMenu()
    {
        menu.SetActive(isOpened);

        partyHolder.PopulateFromList();
        partyHolder.Organize();

        boxesHolder.PopulateFromList();
        boxesHolder.Organize();

        isOpened = !isOpened;
    }
}
