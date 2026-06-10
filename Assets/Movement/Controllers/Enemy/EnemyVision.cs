using System.Threading;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TrainerController>(out var trainer))
        {
            Debug.Log("Treinador visto!");
            EnemyController enemyController = GetComponentInParent<EnemyController>();

            Debug.Log("Perseguindo Carochito");
            enemyController.target = trainer.transform;
            enemyController.stateMachine.ChangeState(enemyController.runState);
        }

        else if (other.TryGetComponent<MonsterController>(out var monster))
        {
            Debug.Log("Carochito visto!");
            EnemyController enemyController = GetComponentInParent<EnemyController>();

            Debug.Log("Perseguindo Carochito");
            enemyController.target = monster.transform;
            enemyController.stateMachine.ChangeState(enemyController.chaseState);
        }
    }
}
