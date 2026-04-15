using System.Collections.Generic;
using UnityEngine;

public class CarochitoPartyHolder : MonoBehaviour
{
    [Header("References")]
    public Transform _contentParent;   
    public GameObject _sheetPrefab;      

    private void Start()
    {
        RefreshParty();
    }

    public void RefreshParty()
    {
        for (int i = _contentParent.childCount - 1; i >= 0; i--)
        {
            Destroy(_contentParent.GetChild(i).gameObject);
        }

        foreach (Carochito member in Party.Instance.carochitos)
        {
            GameObject sheetObj = Instantiate(_sheetPrefab, _contentParent);

            CarochitoPartySheet sheetUI = sheetObj.GetComponent<CarochitoPartySheet>();
            if (sheetUI != null)
            {
                sheetUI.UpdateSheet(member);
            }
            else
            {
                Debug.LogWarning("Sheet prefab is missing SheetUI component.");
            }
        }
    }
}
