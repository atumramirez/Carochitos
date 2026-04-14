using System.Collections.Generic;
using UnityEngine;

public class CarochitoDatabase : MonoBehaviour
{
    [SerializeField] private CarochitoDatabaseAsset database;
    public List<CarochitoBase> Carochitos => database.carochitos;
}
