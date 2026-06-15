using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public static Party Instance;
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

    [Header("Carochitos")]
    [HideInInspector] public Carochito currentCarochito;
    public int currentIndex = 0;
    public List<Carochito> partyCarochitos;

    [Header("Boxes")]
    public List<Carochito> Box1;

    [Header("Menus")]
    public CarochitoSelectionMenu _carochitoSelectionMenu;
    public BoxesMenu _carochitoBoxesMenu;

    private void Start()
    {
        if (partyCarochitos.Count > 0)
        {
            currentCarochito = partyCarochitos[0];
            
        }

        _carochitoSelectionMenu.RefreshMenu();
    }

    public void AddCarochito(Carochito carochito)
    {
        if (partyCarochitos.Count < 5)
        {
            partyCarochitos.Add(carochito);

            if (partyCarochitos.Count == 1)
            {
                currentCarochito = partyCarochitos[0];
            }

            _carochitoBoxesMenu.SetupList(_carochitoBoxesMenu.partyContainer.transform, partyCarochitos);
        }

        else
        {
            Box1.Add(carochito);
        }

        _carochitoSelectionMenu.RefreshMenu();
    }

    public void MoveToParty(Carochito carochito)
    {
        partyCarochitos.Add(carochito);

        _carochitoSelectionMenu.RefreshMenu();
    }

    public void NextCarochito()
    {
        if (partyCarochitos.Count == 0) return;

        currentIndex = (currentIndex + 1) % partyCarochitos.Count;
        currentCarochito = partyCarochitos[currentIndex];

        _carochitoSelectionMenu.RefreshMenu();

        Debug.Log("Proximo Carochito: " + partyCarochitos[currentIndex].Base.Name);
    }

    public void Previous()
    {
        if (partyCarochitos.Count == 0) return;

        currentIndex = (currentIndex - 1 + partyCarochitos.Count) % partyCarochitos.Count;
        currentCarochito = partyCarochitos[currentIndex];
        _carochitoSelectionMenu.RefreshMenu();

        Debug.Log("Carochito Anterior: " + partyCarochitos[currentIndex].Base.Name);
    }
}
