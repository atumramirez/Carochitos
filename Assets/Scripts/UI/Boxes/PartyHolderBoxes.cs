using System.Collections.Generic;
using UnityEngine;

public class PartyHolderBoxes : MonoBehaviour
{
    public void Organize()
    {
        List<Transform> items = new();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i);

            if (slot.childCount > 0)
            {
                items.Add(slot.GetChild(0));
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i);

            if (slot.childCount > 0)
            {
                slot.GetChild(0).SetParent(null);
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetParent(transform.GetChild(i));
            items[i].SetAsLastSibling();
        }
    
    }

    public int GetItemCount()
    {
        int count = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount > 0)
            {
                count++;
            }
        }

        return count;
    }
}
