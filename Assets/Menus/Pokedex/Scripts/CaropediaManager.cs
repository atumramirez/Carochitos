using UnityEngine;

[RequireComponent(typeof(CarochitoDatabase))]
public class CaropediaManager : MonoBehaviour
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
            GameObject instance = Instantiate( _prefab, _caropediaHolder.transform.position, _caropediaHolder.transform.rotation, _caropediaHolder.transform);
            CaropediaSlot slot = instance.GetComponent<CaropediaSlot>();

            slot.carochito = carochito;

            slot.Refresh();
        }
    }
}
