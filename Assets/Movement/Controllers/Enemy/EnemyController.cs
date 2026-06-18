using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyPatrolState : State<MonsterController>
{
    public EnemyPatrolState(MonsterController enemy, StateMachine<MonsterController> sm) : base(enemy, sm)
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void Enter()
    {
        base.Enter();
        character.enemyVision.enabled = true;

        SetNewDestination();
    }

    public override void LogicUpdate()
    {
        if (character.waiting)
        {
            character.waitTimer -= Time.deltaTime;

            if (character.waitTimer <= 0)
            {
                character.waiting = false;
                SetNewDestination();
            }

            return;
        }

        if (!character.navMeshAgent.pathPending && character.navMeshAgent.remainingDistance <= character.navMeshAgent.stoppingDistance)
        {
            character.waiting = true;
            character.waitTimer = Random.Range(character.minWaitTime, character.maxWaitTime);
        }
    }

    private void SetNewDestination()
    {
        Vector3 randomPoint = Random.insideUnitSphere * character.wanderRadius;

        randomPoint += character.transform.position;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, character.wanderRadius, NavMesh.AllAreas))
        {
            character.navMeshAgent.SetDestination(hit.position);
        }
    }

    public override void Exit()
    {
        character.enemyVision.enabled = false;
    }
}

public class EnemyChaseState : State<MonsterController>
{
    public EnemyChaseState(MonsterController enemy, StateMachine<MonsterController> sm) : base(enemy, sm)
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (character.target != null)
        {
            float distanceToTarget = Vector3.Distance(character.transform.position, character.target.position);

            if (distanceToTarget >= character.loseDistance)
            {
                stateMachine.ChangeState(character.enemyPatrolState);
            }

            if (distanceToTarget <= character.attackRange)
            {
                stateMachine.ChangeState(character.attackState);
            }
            
            character.navMeshAgent.SetDestination(character.target.position);
        }
    }
}

public class EnemyRunState : State<MonsterController>
{

    public EnemyRunState(MonsterController enemy, StateMachine<MonsterController> sm) : base(enemy, sm)
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (character.target != null)
        {
            float distanceToTarget = Vector3.Distance(character.transform.position, character.target.position);

            if (distanceToTarget <= character.fleeDistance)
            {
                character.navMeshAgent.speed = character.fleeSpeed;
                FleeFromTarget();
            }
        }
    }

    private void FleeFromTarget()
    {
        Vector3 directionAway = (character.transform.position - character.target.position).normalized;
        Vector3 fleePosition = character.transform.position + directionAway * character.wanderRadius;

        if (NavMesh.SamplePosition(fleePosition, out NavMeshHit hit, character.wanderRadius, NavMesh.AllAreas))
        {
            character.navMeshAgent.SetDestination(hit.position);
        }
    }
}

public class EnemyAttackState : State<MonsterController>
{
    public EnemyAttackState(MonsterController enemy, StateMachine<MonsterController> sm) : base(enemy, sm)
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        float distance = Vector3.Distance(character.transform.position, character.target.position);

        if (distance > character.battleLoseDistance)
        {
            character.stateMachine.ChangeState(character.enemyPatrolState);
            return;
        }

        Vector3 dir = (character.target.position - character.transform.position).normalized;

        if (distance > character.maxCombatDistance)
        {
            character.navMeshAgent.SetDestination(character.target.position);
        }
        else if (distance < character.minCombatDistance)
        {
            Vector3 fleePos = character.transform.position - dir * 2f;

            if (NavMesh.SamplePosition(fleePos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                character.navMeshAgent.SetDestination(hit.position);
            }
        }
        else
        {
            character.navMeshAgent.ResetPath();

            Vector3 lookDirection = character.target.position - character.transform.position;
            lookDirection.y = 0;

            if (lookDirection != Vector3.zero)
            {
                character.transform.rotation = Quaternion.LookRotation(lookDirection);
            }

            character.attackTimer -= Time.deltaTime;

            if (character.attackTimer <= 0f)
            {
                character.RandomAttack();
                character.attackTimer = character.attackCooldown;
            }
        }
    }
}

public class EnemyEatState : State<MonsterController>
{
    public EnemyEatState(MonsterController enemy, StateMachine<MonsterController> sm) : base(enemy, sm)
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (character.eatTimer <= 0f)
        {
            character.navMeshAgent.SetDestination(character.foodPosition.position);

            if (!character.navMeshAgent.pathPending && character.navMeshAgent.remainingDistance <= character.eatDistance)
            {
                character.navMeshAgent.ResetPath();

                character.eatTimer = character.eatDuration;
            }
        }
        else
        {
            character.eatTimer -= Time.deltaTime;

            if (character.eatTimer <= 0f)
            {
                character.FinishEating();
            }
        }

    }
}

public class EnemyInvestigateState : State<MonsterController>
{
    public EnemyInvestigateState(MonsterController enemy, StateMachine<MonsterController> sm) : base(enemy, sm) 
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void Enter()
    {
        base.Enter();

        Vector3 lookdirection = character.soundPosition.position - character.transform.position;

        if (lookdirection != Vector3.zero)
        {
            character.transform.rotation = Quaternion.LookRotation(lookdirection);
        }

        character.navMeshAgent.SetDestination(character.soundPosition.position);
    }

    public override void LogicUpdate()
    {
        character.navMeshAgent.SetDestination(character.soundPosition.position);

        if (!character.navMeshAgent.pathPending && character.navMeshAgent.remainingDistance <= character.navMeshAgent.stoppingDistance)
        {
            character.stateMachine.ChangeState(character.enemyPatrolState);
        }

        return;
    }
}

public class EnemyCaptureState : State<MonsterController>
{
    public EnemyCaptureState(MonsterController enemy, StateMachine<MonsterController> sm) : base(enemy, sm)
    {
        character = enemy;
        stateMachine = sm;
    }

    public override void Enter()
    {
        base.Enter();
        character.navMeshAgent.isStopped = true;
    }
}


