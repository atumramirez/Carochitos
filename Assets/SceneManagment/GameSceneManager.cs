using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    string currentEnviromentScene;

    [SerializeField] Rigidbody playerTransform;

    private void Start()
    {
        DetectCurrentEnviromentScene();
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
        AsyncOperation unload = SceneManager.UnloadSceneAsync(currentEnviromentScene);
        AsyncOperation load = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        currentEnviromentScene = newScene;

        while (unload.isDone == false)
        {
            yield return new WaitForEndOfFrame();
        }

        while (load.isDone == false)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();

        Transform waypoint = FindAnyObjectByType<SceneInfoContainer>().entranceWaypoints[0];

        playerTransform.position = waypoint.position;
        playerTransform.rotation = waypoint.rotation;

        yield return null;
    }
}
