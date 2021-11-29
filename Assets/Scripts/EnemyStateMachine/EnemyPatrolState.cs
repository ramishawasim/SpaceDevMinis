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
        enemy.navMeshAgent.destination = enemy.walkPoint;
        enemy.playerInSightRange = Physics.CheckSphere(enemy.navMeshAgent.transform.position, enemy.sightRange, enemy.whatIsPlayer);
        if (enemy.playerInSightRange) enemy.SwitchState(enemy.ChaseState);
    }
}
