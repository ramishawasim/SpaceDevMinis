using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    private Vector3 enterStartWalkPoint;
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.navMeshAgent.speed = enemy.patrolSpeed;
        enemy.navMeshAgent.angularSpeed = enemy.patrolAngleSpeed;
        enterStartWalkPoint = enemy.walkPoint;
        enemy.navMeshAgent.destination = enterStartWalkPoint;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {        
        enemy.navMeshAgent.destination = enemy.walkPoint;
        if (enemy.playerInSightRange) enemy.SwitchState(enemy.ChaseState);
    }
}
