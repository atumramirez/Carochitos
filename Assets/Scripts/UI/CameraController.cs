using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
  
    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    public void CloseUp()
    {
        cam1.enabled = false;
        cam2.enabled = true;
    }

    public void BodyShot()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }
}
