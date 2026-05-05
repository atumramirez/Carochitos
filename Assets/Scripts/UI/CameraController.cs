using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera[] cameras;

    [Header("Start Camera")]
    public CinemachineCamera startCamera;

    [HideInInspector] public CinemachineCamera currentCamera;

    void Start()
    {
        if (startCamera != null)
        {
            currentCamera = startCamera;
        }
        else
        {
            if (cameras[0] != null)
            {
                currentCamera = cameras[0];
            }
        }
        
        SwitchCamera(currentCamera);
    }

    public void SwitchCamera(CinemachineCamera newCamera)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == newCamera)
            {
                cameras[i].Priority = 2;
            }
            else
            {
                cameras[i].Priority = 1;
            }
        }

        currentCamera = newCamera;
    }
}
