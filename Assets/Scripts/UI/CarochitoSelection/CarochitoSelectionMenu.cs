using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarochitoSelectionMenu : MonoBehaviour
{
    [Header("Menu")]
    public GameObject menu;

    [Header("Main Carochito")]
    public GameObject mainCarochito;
    public Image main;
    public Slider health;
    public Slider exp;

    [Header("Carochito Data")]
    public TextMeshProUGUI _level;
    public TextMeshProUGUI _name;

    [Header("Carochitos in Team")]
    public Transform _contentParent;
    public GameObject _sheetPrefab;


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
        if (Party.Instance.partyCarochitos.Count > 0)
        {
            OpenMenu();

            main.sprite = Party.Instance.currentCarochito.Base.Sprite;

            health.maxValue = Party.Instance.currentCarochito.Health;
            health.value = Party.Instance.currentCarochito.CurrentHealth;

            exp.maxValue = 100;
            exp.value = Party.Instance.currentCarochito.CurrentExp;

            _level.text = "Lv. " + Party.Instance.currentCarochito.Level;
            _name.text = Party.Instance.currentCarochito.Base.Name;

            for (int i = _contentParent.childCount - 1; i >= 0; i--)
            {
                Destroy(_contentParent.GetChild(i).gameObject);
            }

            HashSet<Carochito> addedMonsters = new();

            int currentCarochito = 0;

            foreach (Carochito member in Party.Instance.partyCarochitos)
            {
                if (addedMonsters.Contains(member))
                {
                    continue;
                }
                    
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
