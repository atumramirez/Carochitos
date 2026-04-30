using Unity.Cinemachine;
using UnityEngine;

public class CursorLockToggle : MonoBehaviour
{
    public CameraHandler cameraHandler;

    private CinemachinePanTilt pov;
    private bool cameraControlEnabled = true;

    void Start()
    {
        // Get the POV component from the camera
        pov = cameraHandler.CurrentCamera.GetComponent<CinemachinePanTilt>();

        if (pov == null)
        {
            Debug.LogError("No CinemachinePOV found on the camera!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            cameraControlEnabled = !cameraControlEnabled;

            if (pov != null)
            {
                pov.enabled = cameraControlEnabled;
            }
        }
    }
}