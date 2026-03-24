using System.Collections.Generic;
using UnityEngine;

public class CarochitoPartyHolder : MonoBehaviour
{
    [Header("References")]
    public Transform _contentParent;      // Onde os Carochitos Party Sheets serão istanciados
    public GameObject _sheetPrefab;       // O Prefab de cada Sheet

    public CarochitoParty _carochitoParty;

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

        foreach (Carochito member in _carochitoParty.carochitos)
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
