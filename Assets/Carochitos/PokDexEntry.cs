using UnityEngine;

[System.Serializable]
public class PokeDexEntry
{
    [SerializeField] CarochitoBase _carochitoBase;
    [SerializeField] int _amount;

    public CarochitoBase CarochitoBase { get { return _carochitoBase; } }
    public int Amount { get { return _amount; } }
    public PokeDexEntry(CarochitoBase carochito, int amount)
    {
        _carochitoBase = carochito;
        _amount = amount;
    }
}
