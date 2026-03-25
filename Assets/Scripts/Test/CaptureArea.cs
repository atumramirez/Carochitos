using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    public float lifetime = 1.5f;

    public CarochitoParty playerInventory;

    void Start()
    {
        playerInventory = FindAnyObjectByType<CarochitoParty>();
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Creature>(out var creature))
        {
            playerInventory.AddCarochito(creature.carochito);
            creature.Capture();
        }
    }
}