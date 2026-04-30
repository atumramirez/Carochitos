using Unity.Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("Cameras")]
    public CinemachineCamera[] cameras;

    // Generic Cams
    public CinemachineCamera thirdPersonCam;

    // Player Camns
    public CinemachineCamera combatCam;

    // Monster Cams

    // Generic Info
    public CinemachineCamera startCamera;
    private CinemachineCamera currentCamera;
    public CinemachineCamera CurrentCamera { get { return currentCamera; } }

    private Transform combatCameraTransform;

    public void Initialize()
    {
        currentCamera = startCamera;

        SwitchCamera(currentCamera);
    }


    public void SwitchCamera(CinemachineCamera newCamera)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == newCamera)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }

        currentCamera = newCamera;
    }

    public void LookAt(Transform newTransform)
    {
        currentCamera.Follow = newTransform;
        currentCamera.LookAt = newTransform;
    }
}
