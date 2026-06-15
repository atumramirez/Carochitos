using System.Collections.Generic;
using UnityEngine;

public class BattleArena : MonoBehaviour
{
    public List<Carochito> carochitos;

    private int currentEnemyIndex = 0;
    public Transform spawnPoint;

    private GameObject currentEnemy;

    public BoxCollider _arenaBoundaries;

    [Header("Action Graphs")]
    public RuntimeDialogueGraph dialogueGraph;

    void OnCollisionEnter(Collision collision)
    {
        Vector3 center = transform.position;

        Vector3 direction =
            (collision.transform.position - center).normalized;

        float dot = Vector3.Dot(
            collision.relativeVelocity.normalized,
            direction);

        if (dot < 0)
        {
            Physics.IgnoreCollision(
                collision.collider,
                GetComponent<Collider>());
        }
    }

    public void Start()
    {
        _arenaBoundaries.enabled = false;
    }

    public void StartBattle()
    {
        //_arenaBoundaries.enabled = true;
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (currentEnemyIndex >= carochitos.Count)
        {
            EndBattle();
            return;
        }

        GameObject currentEnemy = Instantiate(carochitos[currentEnemyIndex].Base.Model, spawnPoint.position, spawnPoint.rotation);
        currentEnemy.GetComponent<CarochitoBattler>().SetUp(carochitos[currentEnemyIndex].Base, carochitos[currentEnemyIndex].Level, isCapturable: false);
        Setup(currentEnemy.GetComponent<CarochitoBattler>());

        currentEnemyIndex++;
    }

    private void Setup(CarochitoBattler enemy)
    {
        enemy.SetUp(OnEnemyDied);
    }

    private void OnEnemyDied()
    {
        SpawnEnemy();
    }

    public void EndBattle()
    {
        //_arenaBoundaries.enabled = false;

        if (dialogueGraph != null)
        {
            ActionManager.Instance.OpenGraph(dialogueGraph);
        }
    }
}
