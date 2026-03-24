using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarochitoBoxes : MonoBehaviour
{
    [SerializeField] List<Carochito> Box1;
    public void AddCarochito(Carochito carochito)
    {
        /// Adicionar lógica de como funcionam as boxes
        Box1.Add(carochito);

    }
}
