using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    string currentEnviromentScene;

    [SerializeField] Rigidbody playerTransform;
    [SerializeField] GameObject loadingCanvas;

    private void Start()
    {
        DetectCurrentEnviromentScene();

        if (loadingCanvas != null)
        {
            loadingCanvas.SetActive(false);
        }
    }

    public void DetectCurrentEnviromentScene()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.name == "Essential")
            {
                continue;
            }

            currentEnviromentScene = scene.name;
        }
    }

    string newScene;

    public void SwitchEnviromentScene(string newScene)
    {
        this.newScene = newScene;

        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
        AsyncOperation unload = null;
        AsyncOperation load = null;

        if (currentEnviromentScene != null) 
        {
            unload = SceneManager.UnloadSceneAsync(currentEnviromentScene);
        }

        if (newScene != null)
        {
            load = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

            currentEnviromentScene = newScene;
        }

        if (loadingCanvas != null)
        {
            loadingCanvas.SetActive(true);
        }

        if (unload != null)
        {
            while (unload.isDone == false)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        if (load != null)
        {
            while (load.isDone == false)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        
        yield return new WaitForEndOfFrame();

        if (loadingCanvas != null)
        {
            loadingCanvas.SetActive(false);
        }

        SceneInfoContainer info = FindAnyObjectByType<SceneInfoContainer>();

        if (info != null && info.entranceWaypoints != null && info.entranceWaypoints.Count > 0)
        {
            Transform waypoint = info.entranceWaypoints[0]; 

            if (waypoint != null && playerTransform != null)
            {
                playerTransform.position = waypoint.position;
                playerTransform.rotation = waypoint.rotation;
            }
        }
        else
        {
            Debug.Log("SceneInfoContainer or entrance waypoint missing!");
        }

        yield return null;
    }
}
