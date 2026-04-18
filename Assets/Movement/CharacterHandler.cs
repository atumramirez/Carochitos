using Unity.Cinemachine;
using UnityEngine;
public class CharacterHandler : MonoBehaviour
{
    [Header("Available Characters")]
    public TrainerController tarinerCharacter;
    //public MonsterController monsterController = null;
    public GenericController currentCharacter;

    [Header("MonsterData")]
    public bool isMonsterOut = false;
    public GameObject monster;
    public GameObject prefab;
    public Transform spawnPoint;

    [Header("Menu")]
    public GameObject monsterHUD;
    public GameObject playerHUD;

    [Header("Camera")]
    public CinemachineCamera cam;

    private void Start()
    {
        currentCharacter = tarinerCharacter;

        /*
        if (monsterController != null)
        {
            monster = monsterController.gameObject;
        }
        */
    }

    public void Summon()
    {
        if(isMonsterOut == false)
        {
            monster = Instantiate(
                Party.Instance.currentCarochito.Base.Model,
                spawnPoint.position,
                spawnPoint.rotation
             );

            //monsterController = monster.GetComponent<MonsterController>();

            isMonsterOut = true;
        }
        else
        {
            Destroy(monster);

            isMonsterOut = false;
            monster = null;
            //monsterController = null;
        }
    }

    public void TakeAway()
    {
        //tarinerCharacter.stateMachine.ChangeState(tarinerCharacter.summonState);
    }

    public void SwapCharacter()
    {
        if (isMonsterOut == false) return;

        if (currentCharacter == tarinerCharacter)
        {
            // tarinerCharacter.stateMachine.ChangeState(tarinerCharacter.aiState);
            //monsterController.stateMachine.ChangeState(monsterController.standingState);

            //currentCharacter = monsterController;

            monsterHUD.SetActive(true);
            playerHUD.SetActive(false);
        }
        else
        {
            tarinerCharacter.stateMachine.ChangeState(tarinerCharacter.standing);
            //monsterController.stateMachine.ChangeState(monsterController.monsterAiState);

            currentCharacter = tarinerCharacter;

            monsterHUD.SetActive(false);
            playerHUD.SetActive(true);
        }

        // Update camera logic later
        cam.LookAt = currentCharacter.transform;
        cam.Follow = currentCharacter.transform;
    }
}
