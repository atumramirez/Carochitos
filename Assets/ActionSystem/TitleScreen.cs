using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject titleScreen;
    public GameSceneManager gameSceneManager;
    public void NewGame()
    {
        gameSceneManager.SwitchEnviromentScene("SalaDeAula", 0);
        titleScreen.SetActive(false);
    }
}
