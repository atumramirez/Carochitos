using System.Threading;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public BoxCollider _boxCollider;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.TryGetComponent<MonsterController>(out var monster))
        {
            Debug.Log("Carochito visto!");
            MonsterController enemyController = GetComponentInParent<MonsterController>();

            Debug.Log("Perseguindo Carochito");
            enemyController.target = monster.transform;
            enemyController.stateMachine.ChangeState(enemyController.enemyChaseState);
        }
        if (other.TryGetComponent<BerlinerBall>(out var ball))
        {
            Debug.Log("Carochito visto!");
            MonsterController enemyController = GetComponentInParent<MonsterController>();

            Debug.Log("Perseguindo Carochito");
            enemyController.foodPosition = ball.transform;
            enemyController.Eat(ball.transform);
        }
    }
}
