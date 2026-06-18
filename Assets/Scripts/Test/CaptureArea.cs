using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    public TrainerController _trainer;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CarochitoBattler>(out var creature))
        {
            if (creature._isCapturable == true && creature._isGettingCaptured != true)
            {
                if (_trainer != null)
                {
                    _trainer.Capture(creature);
                }
            }
        }
    }
}