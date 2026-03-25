using System;
using UnityEngine;

public class BoxesMenu : MonoBehaviour
{
    public GameObject prefab;
    public GameObject Boxes;

    public CarochitoParty carochitoParty;

    private void Start()
    {
        Refresh(); 
    }

    public void Refresh()
    {
        int index = 0;

        if (carochitoParty != null)
        {
            foreach (Transform slot in Boxes.transform)
            {
                if (index >= carochitoParty.carochitos.Count)
                    break;

                GameObject instance = Instantiate(prefab, slot);
                DragItem dragItem = prefab.GetComponent<DragItem>();
                dragItem.Refresh(carochitoParty.carochitos[index]);

                RectTransform rect = instance.GetComponent<RectTransform>();
                rect.anchoredPosition = Vector2.zero;
                rect.localScale = Vector3.one;

                index++;
            }
        }
    }
}
