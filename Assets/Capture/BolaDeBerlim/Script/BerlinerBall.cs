using UnityEngine;

public class BerlinerBall : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasLanded = false;

    public Flavour flavour;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Land();
        }
    }

    public void SetFlavour(Flavour _flavour)
    {
        flavour = _flavour;
    }

    public void Land()
    {
        hasLanded = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
    }
}
