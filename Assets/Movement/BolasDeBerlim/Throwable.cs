using UnityEngine;

public class Throwable : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasLanded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Land();
            }
        }
    }

    void Land()
    {
        hasLanded = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
    }
}
