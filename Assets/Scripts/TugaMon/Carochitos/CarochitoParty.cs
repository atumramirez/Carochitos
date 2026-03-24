using System.Collections.Generic;
using UnityEngine;

public class CarochitoParty : MonoBehaviour
{
    [SerializeField] public List<Carochito> carochitos;
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
        /// Adicionar Animação de UI
        /// [Animação de Captura]
        /// 

        if(carochitos.Count < 6)
        {
            carochitos.Add(carochito);
        }

        else
        {
            carochitoBoxes.AddCarochito(carochito);
        }
    }

    public CarochitoBoxes carochitoBoxes;

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
