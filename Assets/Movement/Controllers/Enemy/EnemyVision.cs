using System.Threading;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public BoxCollider _boxCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TrainerController>(out var trainer))
        {
            Debug.Log("Treinador visto!");
            MonsterController enemyController = GetComponentInParent<MonsterController>();

            Debug.Log("Perseguindo Carochito");
            enemyController.target = trainer.transform;
            enemyController.stateMachine.ChangeState(enemyController.enemyChaseState);
        }
        
        if (other.TryGetComponent<MonsterController>(out var monster))
        {
            Debug.Log("Carochito visto!");
            MonsterController enemyController = GetComponentInParent<MonsterController>();

            Debug.Log("Perseguindo Carochito");
            enemyController.target = monster.transform;
            enemyController.stateMachine.ChangeState(enemyController.enemyChaseState);
        }
    }
}
