using UnityEngine;

public class BoxesHolder : BoxesAndPartyHolder
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

        for (int i = 0; i < CarochitoBoxes.Instance.Box1.Count && i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i);

            if (CarochitoBoxes.Instance.Box1[i].Base == null)
            {
                continue;
            }
            else
            {
                GameObject newItem = Instantiate(prefab, slot);
                newItem.transform.SetAsLastSibling();

                newItem.GetComponent<DragItem>().Refresh(CarochitoBoxes.Instance.Box1[i]);
            }
        }
    }

    public override void PopulateToList()
    {
        CarochitoBoxes.Instance.Box1.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i);

            if (slot.childCount > 0)
            {
                DragItem data = slot.GetChild(0).GetComponent<DragItem>();
                CarochitoBoxes.Instance.AddCarochito(data.carochito);
            }
            else
            {
                CarochitoBoxes.Instance.Box1.Add(null);
            }
        }
    }
}
