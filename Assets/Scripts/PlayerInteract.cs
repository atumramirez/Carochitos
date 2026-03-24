using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform _interactArea;
    [SerializeField] private float _interactAreaSize = 1f;
    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(_interactArea.position, _interactAreaSize);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.OnInteract();
                break;
            }
        }
    }
}
