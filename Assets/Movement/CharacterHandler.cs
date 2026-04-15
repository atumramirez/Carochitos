using Unity.Cinemachine;
using UnityEngine;
public class CharacterHandler : MonoBehaviour
{
    [Header("Available Characters")]
    public TrainerController tarinerCharacter;
    public MonsterController monsterController = null;
    public GenericController currentCharacter;

    [Header("MonsterData")]
    public bool isMonsterOut = false;
    public GameObject prefab;
    public Transform spawnPoint;

    [Header("Camera")]
    public CinemachineCamera cam;

    private void Start()
    {
        currentCharacter = tarinerCharacter;
    }

    public void Summon()
    {
        if(isMonsterOut == false)
        {
            GameObject monster = Instantiate(
                Party.Instance.currentCarochito.Base.Model,
                spawnPoint.position,
                spawnPoint.rotation
             );

            monsterController = monster.GetComponent<MonsterController>();

            isMonsterOut = true;
        }
        else
        {
            Debug.Log("Vou me atirar");
            isMonsterOut = false;
        }
    }

    public void SwapCharacter()
    {
        if (isMonsterOut == false) return;

        if (currentCharacter == tarinerCharacter)
        {
            tarinerCharacter.movementSM.ChangeState(tarinerCharacter.aiState);
            monsterController.movementSM.ChangeState(monsterController.standingState);

            currentCharacter = monsterController;
        }
        else
        {
            tarinerCharacter.movementSM.ChangeState(tarinerCharacter.standing);
            monsterController.movementSM.ChangeState(monsterController.monsterAiState);

            currentCharacter = tarinerCharacter;
        }

        // Update camera logic later
        cam.LookAt = currentCharacter.transform;
        cam.Follow = currentCharacter.transform;
    }
}
