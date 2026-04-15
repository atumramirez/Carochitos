using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxesMenu : MonoBehaviour
{
    [Header("Containers")]
    public GameObject boxesContainer;
    public GameObject partyContainer;

    [Header("")]
    public GameObject monsterSlotPrefab;

    private void Start()
    {
        SetupList(partyContainer.transform, Party.Instance.carochitos);
        SetupList(boxesContainer.transform, Boxes.Instance.Box1);

        Organize(boxesContainer.transform);
    }

    public void Organize(Transform container) // Coloca em ordem (sem deixar espaços em branco) a Children do Container selecionado
    {
        List<Transform> items = new();

        for (int i = 0; i < container.transform.childCount; i++)
        {
            Transform slot = container.transform.GetChild(i);

            if (slot.childCount > 0)
            {
                items.Add(slot.GetChild(0));
            }
        }

        for (int i = 0; i < container.transform.childCount; i++)
        {
            Transform slot = container.transform.GetChild(i);

            if (slot.childCount > 0)
            {
                slot.GetChild(0).SetParent(null);
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetParent(container.transform.GetChild(i));
            items[i].SetAsLastSibling();
        }
    }

    public int GetItemCount(Transform container) // Conta a quantidade de Children que tem o container
    {
        int count = 0;

        for (int i = 0; i < container.transform.childCount; i++)
        {
            if (container.transform.GetChild(i).childCount > 0)
            {
                count++;
            }
        }

        return count;
    }

    public void SetupList(Transform container, List<Carochito> carochitos)
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            Transform slot = container.transform.GetChild(i);

            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }

        for (int i = 0; i < carochitos.Count && i < container.transform.childCount; i++)
        {
            if (carochitos[i] == null)
                continue;

            Transform slot = container.transform.GetChild(i);

            GameObject newItem = Instantiate(monsterSlotPrefab, slot);
            newItem.transform.SetAsLastSibling();

            newItem.GetComponent<MonsterDragItem>().boxesMenu = this;

            if (container == partyContainer.transform)
            {
                newItem.GetComponent<MonsterDragItem>().Setup(carochitos[i], true);
            }
            else if (container == boxesContainer.transform)
            {
                newItem.GetComponent<MonsterDragItem>().Setup(carochitos[i], false);
            }
            else
            {
                Debug.Log("Não sei para onde foi!");
            }
        }
    }

    public void SaveList(Transform container, List<Carochito> carochitos)
    {
        carochitos.Clear();

        for (int i = 0; i < container.transform.childCount; i++)
        {
            Transform slot = container.transform.GetChild(i);

            if (slot.childCount > 0)
            {
                MonsterDragItem data = slot.GetChild(0).GetComponent<MonsterDragItem>();

                if (carochitos == Party.Instance.carochitos)
                {
                    Party.Instance.MoveToParty(data.carochito);
                }
                else
                {
                    Boxes.Instance.AddCarochito(data.carochito);
                }
            }
        }
    }


}
