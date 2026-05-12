using UnityEngine;

public class CarochitoBattler : MonoBehaviour
{
    [SerializeField] CarochitoBase _base;
    [SerializeField] int _level;

    public Carochito Carochito { get; set; }

    public void SetUp()
    {
        Carochito = new Carochito( _base, _level);
    }
}
