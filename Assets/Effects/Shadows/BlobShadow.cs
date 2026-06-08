using UnityEngine;

public class BlobShadow : MonoBehaviour
{
    public GameObject shadow;
    public Transform Transform;
    public RaycastHit hit;
    public Vector3 offset = new();

    private void FixedUpdate()
    {
        Ray downRay = new(new Vector3(Transform.position.x + offset.x, Transform.position.y + offset.y, Transform.position.z + offset.z), -Vector3.up);

        // gets the hit from the raycast and converts it unto a vector3
        Vector3 hitPosition = hit.point;

        // transofrm the shadow to the location
        shadow.transform.position = hitPosition;

        // Cast a ray straight downwards, reads back where it lands (this is optional but reccomended)
        if (Physics.Raycast(downRay, out hit))
        {
            print(hit.transform);
        }
    }

}
