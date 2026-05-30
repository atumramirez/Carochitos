using System.Collections.Generic;
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

    [Header("Menu")]
    public GameObject menu;

    [Header("Main Carochito")]
    public GameObject mainCarochito;
    public Image main;

    [Header("Carochito Data")]
    public TextMeshProUGUI _level;
    public TextMeshProUGUI _name;

    [Header("Bottom Panel")]
    public Transform _contentParent;

    [Header("Sheet Prefab")]
    public GameObject _sheetPrefab;

    public void Start()
    {
        RefreshMenu();
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void RefreshMenu()
    {
        if (Party.Instance.carochitos.Count > 0)
        {
            OpenMenu();

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

            int currentCarochito = 0;

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

                    if (currentCarochito == Party.Instance.currentIndex)
                    {
                        sheetUI.Selected(true);
                    }
                    else
                    {
                        sheetUI.Selected(false);
                    }
                }
                else
                {
                    Debug.LogWarning("Sheet prefab is missing MonsterSelectionSheet component.");
                }

                currentCarochito++;
            }
        }
        else
        {
            CloseMenu();
        }
    }
}
