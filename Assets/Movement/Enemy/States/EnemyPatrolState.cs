using UnityEngine;

public class EnemyPatrolState : State<EnemyController>
{
    private Vector3 walkPoint;
    private bool walkPointSet;

    public EnemyPatrolState(EnemyController enemy, StateMachine<EnemyController> sm) : base(enemy, sm) 
    { 

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (character.CanSeeTarget(character.player, character.sightRange, character.whatIsPlayer))
        {
            stateMachine.ChangeState(character.chaseState);
            return;
        }

        if (character.CanSeeTarget(character.ball, character.sightRange, character.whatIsBall))
        {
            stateMachine.ChangeState(character.investigateState);
            return;
        }

        Patrol();
    }

    void Patrol()
    {
        if (!walkPointSet) Search();

        if (walkPointSet)
        {
            character.agent.SetDestination(walkPoint);

            if (Vector3.Distance(character.transform.position, walkPoint) < 1f)
                walkPointSet = false;
        }
    }

    void Search()
    {
        float randomZ = Random.Range(-character.walkPointRange, character.walkPointRange);
        float randomX = Random.Range(-character.walkPointRange, character.walkPointRange);

        walkPoint = character.transform.position + new Vector3(randomX, 0, randomZ);

        if (Physics.Raycast(walkPoint, Vector3.down, 2f, character.whatIsGround))
            walkPointSet = true;
    }
}
