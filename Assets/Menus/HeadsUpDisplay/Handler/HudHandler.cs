using UnityEngine;

public class HudHandler : MonoBehaviour
{
    public GameObject TrainerHud;
    public GameObject MonsterHud;

    public void OpenTrainerHud()
    {
        TrainerHud.SetActive(true);
        MonsterHud.SetActive(false);
    }

    public void OpenMonterHud()
    {
        TrainerHud.SetActive(false);
        MonsterHud.SetActive(true);
    }
}
