using UnityEngine;

public class BoxesMenu : MonoBehaviour
{
    public bool isOpened = false;
    public GameObject menu;

    private void Start()
    {
        ActivateMenu();
    }
    public void ActivateMenu()
    {
        menu.SetActive(isOpened);

        PartyHolder.Instance.PopulateFromList();
        PartyHolder.Instance.Organize();

        BoxesHolder.Instance.PopulateFromList();
        //BoxesHolder.Instance.Organize();

        Debug.Log("DRADSPA");

        isOpened = !isOpened;
    }
}
