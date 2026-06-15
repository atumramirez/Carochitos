using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("Cameras")]
    public CinemachineCamera[] cameras;

    [Header("Generic Cams")]
    public CinemachineCamera thirdPersonCam;
    public CinemachineCamera insideCam;
    public CinemachineCamera combatCam;

    [Header("Costumization Cameras")]
    public CinemachineCamera bodyCam;
    public CinemachineCamera faceCam;

    [Header("Current Camera")]
    [SerializeField] private CinemachineCamera currentCamera;
    public CinemachineCamera extraCam;
    public CinemachineCamera CurrentCamera { get { return currentCamera; } }

    [Header("Enviromental Cam")]
    public CinemachineCamera _sceneCamera;

    public void Initialize(Enviroment enviroment)
    {
        switch (enviroment)
        {
            case Enviroment.Outiside:
                currentCamera = thirdPersonCam;
                _sceneCamera = thirdPersonCam;
                break;
            case Enviroment.Inside:
                currentCamera = insideCam;
                _sceneCamera = insideCam;
                break;
        }

        SwitchCamera(currentCamera);
    }

    public void SwitchCamera(CinemachineCamera newCamera)
    {
        extraCam = null;

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

    public void LookAt(Transform newTransform)
    {
        currentCamera.Follow = newTransform;

        //
        if (newTransform.GetComponent<GenericController>() != null)
        {
            currentCamera.LookAt = newTransform.GetComponent<GenericController>().headPivot;
        }
        else
        {
            currentCamera.LookAt = newTransform;
        }
        
    }

    public void SwitchToCamera(CinemachineCamera newCamera)
    {
        extraCam = newCamera;

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = 1;
        }

        extraCam.Priority = 2;

        currentCamera = extraCam;
    }

   
}
