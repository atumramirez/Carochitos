using UnityEngine;

public class EnemyHud : MonoBehaviour
{
    public Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }
}
