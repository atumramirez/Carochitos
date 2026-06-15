using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    public Party playerInventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CarochitoBattler>(out var creature))
        {
            if (creature._isCapturable == true && creature._isGettingCaptured != true)
            {
                creature.Capture();
            }
        }
    }
}