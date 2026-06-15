using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    string currentEnviromentScene;

    [SerializeField] CharacterController playerTransform;
    [SerializeField] GameObject loadingCanvas;

    public Enviroment _currentEnviroment;
    public TrainerController _trainer;

    public BookMenu _bookMenu;

    private void Start()
    {
        DetectCurrentEnviromentScene();

        if (loadingCanvas != null)
        {
            loadingCanvas.SetActive(false);
        }
    }

    public void StartGame()
    {
        _bookMenu.OpenBook(1);
        SwitchEnviromentScene("SalaDeAula", 0);
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
    int newWaypoint;

    public void SwitchEnviromentScene(string scene, int waypoint)
    {
        this.newScene = scene;
        this.newWaypoint = waypoint;

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

        SceneInfoContainer info = FindAnyObjectByType<SceneInfoContainer>();

        if (info != null && info.entranceWaypoints != null && info.entranceWaypoints.Count > 0)
        {
            Transform waypoint = info.entranceWaypoints[newWaypoint];

            Debug.Log($"Waypoint Position: {waypoint.position}");
            Debug.Log($"Player Position Before: {playerTransform.transform.position}");


            if (waypoint != null && playerTransform != null)
            {
                playerTransform.enabled = false;

                playerTransform.transform.SetPositionAndRotation(
                    waypoint.position,
                    waypoint.rotation
                );

                playerTransform.enabled = true;

                Debug.Log($"Player Position After: {playerTransform.transform.position}");
            }

            _currentEnviroment = info.enviroment;
            _trainer.RefreshCamera();
        }
        else
        {
            Debug.Log("SceneInfoContainer or entrance waypoint missing!");
        }

        
        yield return new WaitForSeconds(1f);

        yield return new WaitForEndOfFrame();

        if (loadingCanvas != null)
        {
            loadingCanvas.SetActive(false);
        }

        if (ActionManager.Instance.CurrentNode is TeleportAction)
        {
            ActionManager.Instance.EndAction();
        }

        yield return null;
    }
}
