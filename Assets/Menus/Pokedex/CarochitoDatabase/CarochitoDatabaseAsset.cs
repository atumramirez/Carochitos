using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Carochitos/Database")]
public class CarochitoDatabaseAsset : ScriptableObject
{
    public List<CarochitoBase> carochitos;
}