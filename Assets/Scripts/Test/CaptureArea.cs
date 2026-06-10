using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    public Party playerInventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CarochitoBattler>(out var creature))
        {
            Debug.Log("Acertaste");
            playerInventory.AddCarochito(creature.Carochito);
            creature.Capture();
        }
    }
}