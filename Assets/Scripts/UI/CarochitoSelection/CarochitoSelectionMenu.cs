using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarochitoSelectionMenu : MonoBehaviour
{
    public static CarochitoSelectionMenu instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public GameObject mainCarochito;
    public Image main;
    public TextMeshProUGUI _level;
    public TextMeshProUGUI _name;

    public Transform _contentParent; 
    public GameObject _sheetPrefab;

    public void OpenMenu()
    { }

    public void CloseMenu()
    { }

    public void RefreshMenu()
    {
        main.sprite = Party.Instance.currentCarochito.Base.Sprite;

        _level.text = "Lv. " + Party.Instance.currentCarochito.Level;
        _name.text = Party.Instance.currentCarochito.Base.Name;

        // Step 1: Clear existing UI elements
        for (int i = _contentParent.childCount - 1; i >= 0; i--)
        {
            Destroy(_contentParent.GetChild(i).gameObject);
        }

        // Step 2: Track already added monsters (to prevent duplicates)
        HashSet<Carochito> addedMonsters = new();

        foreach (Carochito member in Party.Instance.carochitos)
        {
            // Skip duplicates
            if (addedMonsters.Contains(member))
                continue;

            addedMonsters.Add(member);

            GameObject sheetObj = Instantiate(_sheetPrefab, _contentParent);

            if (sheetObj.TryGetComponent<CarochitoSelectionSheet>(out var sheetUI))
            {
                sheetUI.UpdateSheet(member);
            }
            else
            {
                Debug.LogWarning("Sheet prefab is missing MonsterSelectionSheet component.");
            }
        }
    }
}
