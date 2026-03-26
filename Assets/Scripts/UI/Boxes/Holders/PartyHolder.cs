using UnityEngine;

public class PartyHolder : BoxesAndPartyHolder
{
    public static PartyHolder Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Transform _menu;
    public override void PopulateFromList()
    {
        for (int i = 0; i < _menu.childCount; i++)
        {
            Transform slot = _menu.GetChild(i);

            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }

        for (int i = 0; i < CarochitoParty.Instance.carochitos.Count && i < _menu.childCount; i++)
        {
            if (CarochitoParty.Instance.carochitos[i] == null)
                continue;

            Transform slot = _menu.GetChild(i);

            GameObject newItem = Instantiate(prefab, slot);
            newItem.transform.SetAsLastSibling();

            newItem.GetComponent<DragItem>().Refresh(CarochitoParty.Instance.carochitos[i]);
        }
    }

    public override void PopulateToList()
    {
        CarochitoParty.Instance.carochitos.Clear();

        for (int i = 0; i < _menu.childCount; i++)
        {
            Transform slot = _menu.GetChild(i);

            if (slot.childCount > 0)
            {
                DragItem data = slot.GetChild(0).GetComponent<DragItem>() ;
                CarochitoParty.Instance.MoveToParty(data.carochito);
            }
        }
    }

    public override int GetItemCount()
    {
        int count = 0;

        for (int i = 0; i < _menu.childCount; i++)
        {
            if (_menu.GetChild(i).childCount > 0)
            {
                count++;
            }
        }
        return count;
    }
}
