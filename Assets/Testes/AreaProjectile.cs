using UnityEngine;

public class AreaProjectile : MonoBehaviour
{
    public float speed = 3f;
    public float maxDistance = 20f;
    public float lifetime = 5f;

    private Vector3 startPosition;

    void Start()
    {
        Destroy(gameObject, lifetime);
        startPosition = transform.position;
    }

    void Update()
    {
        // Move forward
        transform.position += transform.up * speed * Time.deltaTime;

        // Check traveled distance
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);

        if (distanceTravelled >= maxDistance)
        {
            speed = 0;
        }
    }
}
