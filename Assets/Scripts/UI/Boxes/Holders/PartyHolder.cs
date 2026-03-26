using UnityEngine;

public class PartyHolder : BoxesAndPartyHolder
{
    public override void PopulateFromList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i);

            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }

        for (int i = 0; i < CarochitoParty.Instance.carochitos.Count && i < transform.childCount; i++)
        {
            if (CarochitoParty.Instance.carochitos[i] == null)
                continue;

            Transform slot = transform.GetChild(i);

            GameObject newItem = Instantiate(prefab, slot);
            newItem.transform.SetAsLastSibling();

            newItem.GetComponent<DragItem>().Refresh(CarochitoParty.Instance.carochitos[i]);
        }
    }

    public override void PopulateToList()
    {
        CarochitoParty.Instance.carochitos.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i);

            if (slot.childCount > 0)
            {
                DragItem data = slot.GetChild(0).GetComponent<DragItem>() ;
                CarochitoParty.Instance.MoveToParty(data.carochito);
            }
        }
    }
}
