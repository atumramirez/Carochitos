using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    public float lifetime = 1.5f;

    public PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Creature>(out var creature))
        {
            playerInventory.AddCreature(creature);
            creature.Capture();
        }
    }
}