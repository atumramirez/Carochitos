using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfoContainer : MonoBehaviour
{
    public List<Transform> entranceWaypoints;

    public void ChangeScene(string targetScene)
    {
        FindAnyObjectByType<GameSceneManager>().SwitchEnviromentScene(targetScene, 0);
    }
}
