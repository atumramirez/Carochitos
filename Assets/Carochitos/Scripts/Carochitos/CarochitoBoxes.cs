using System.Collections.Generic;
using UnityEngine;

public class CarochitoBoxes : MonoBehaviour
{
    public static CarochitoBoxes Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] public List<Carochito> Box1;
    public void AddCarochito(Carochito carochito)
    {
        Box1.Add(carochito);
    }
}
