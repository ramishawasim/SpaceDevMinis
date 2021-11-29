using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.navMeshAgent.speed = enemy.chaseSpeed;
        enemy.navMeshAgent.angularSpeed = enemy.chaseAngleSpeed;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {        
        enemy.navMeshAgent.destination = enemy.playerPositionTransform.position;
        if (!enemy.playerInSightRange) enemy.SwitchState(enemy.PatrolState);
    }
}
