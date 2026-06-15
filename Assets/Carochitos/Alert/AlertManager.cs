
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public GameObject _alertContainer;
    public List<GameObject> _alerts = new();
    public GameObject _alertPrefab;

    public static AlertManager instance;

    public void Awake()
    {
        instance = this;
    }

    public void AddAlert(Carochito carochito, string text)
    {
        GameObject newAlert = Instantiate(_alertPrefab, _alertContainer.transform);

        newAlert.GetComponent<AlertCommand>().SetUp(carochito, text);
        _alerts.Add(newAlert);
    }
}
