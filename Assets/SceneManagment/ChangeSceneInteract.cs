using UnityEngine;

public class ChangeSceneInteract : MonoBehaviour
{
    [SerializeField] string targetScene;

    public void ChangeScene()
    {
        FindAnyObjectByType<GameSceneManager>().SwitchEnviromentScene(targetScene);
    }
}
