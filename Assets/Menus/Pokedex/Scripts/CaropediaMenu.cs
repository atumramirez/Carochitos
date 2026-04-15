using UnityEngine;

[RequireComponent(typeof(CarochitoDatabase))]
public class CaropediaMenu : MonoBehaviour
{
    public GameObject _caropediaHolder;
    public GameObject _prefab;
    public CarochitoDatabase carochitoDatabase;

    private void Start()
    {
        carochitoDatabase = GetComponent<CarochitoDatabase>();
        Refresh();
    }

    public void Refresh()
    {
        foreach (var carochito in carochitoDatabase.Carochitos)
        {
            GameObject instance = Instantiate( _prefab, _caropediaHolder.transform);

            CaropediaSlot slot = instance.GetComponent<CaropediaSlot>();


            slot.carochito = carochito;

            slot.Setup(carochito.IsCaptured);
        }
    }
}
