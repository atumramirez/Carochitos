using System.Collections.Generic;
using UnityEngine;

public class CarochitoParty : MonoBehaviour
{
    [SerializeField] List<Carochito> carochitos;

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
}
