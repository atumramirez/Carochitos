using System.Collections.Generic;
using UnityEngine;

public class CarochitoParty : MonoBehaviour
{
    public static CarochitoParty Instance;

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

    public List<Carochito> carochitos;

    public Carochito currentCarochito;
    public int currentIndex = 0;

    public CarochitoSelectionMenu _carochitoSelectionMenu;

    private void Start()
    {
        currentCarochito = carochitos[0];
        _carochitoSelectionMenu.RefreshMenu();
    }

    public void AddCarochito(Carochito carochito)
    {
        if (carochitos.Count < 6)
        {
            carochitos.Add(carochito);
        }

        else
        {
            CarochitoBoxes.Instance.AddCarochito(carochito);
        }

        CarochitoSelectionMenu.instance.RefreshMenu();


        // [Adicionar Logica da PokéDex]
    }

    public void MoveToParty(Carochito carochito)
    {
        carochitos.Add(carochito);
    }

    public void NextCarochito()
    {
        if (carochitos.Count == 0) return;

        currentIndex = (currentIndex + 1) % carochitos.Count;
        currentCarochito = carochitos[currentIndex];
        _carochitoSelectionMenu.RefreshMenu();

        Debug.Log("Next item: " + carochitos[currentIndex].Base.Name);
    }

    public void Previous()
    {
        if (carochitos.Count == 0) return;

        currentIndex = (currentIndex - 1 + carochitos.Count) % carochitos.Count;
        currentCarochito = carochitos[currentIndex];
        _carochitoSelectionMenu.RefreshMenu();

        Debug.Log("Previous item: " + carochitos[currentIndex].Base.Name);
    }
}
