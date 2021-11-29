using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.navMeshAgent.speed = enemy.patrolSpeed;
        enemy.navMeshAgent.angularSpeed = enemy.patrolAngleSpeed;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.navMeshAgent.CalculatePath(enemy.walkPoint, enemy.path);
        if (enemy.path.status == NavMeshPathStatus.PathComplete)
        {
            enemy.navMeshAgent.destination = enemy.walkPoint;
        }
        enemy.playerInSightRange = Physics.CheckSphere(enemy.navMeshAgent.transform.position, enemy.sightRange, enemy.whatIsPlayer);
        if (enemy.playerInSightRange) enemy.SwitchState(enemy.ChaseState);
    }
}
