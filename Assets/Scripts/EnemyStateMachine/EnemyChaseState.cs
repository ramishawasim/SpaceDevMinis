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
        enemy.navMeshAgent.CalculatePath(enemy.playerPositionTransform.position, enemy.path);
        if (enemy.path.status == NavMeshPathStatus.PathComplete)
        {
            enemy.navMeshAgent.destination = enemy.playerPositionTransform.position;
        }
        else
        {
            // enemy.navMeshAgent.transform.LookAt(enemy.playerPositionTransform.position);
        }
        enemy.playerInSightRange = Physics.CheckSphere(enemy.navMeshAgent.transform.position, enemy.sightRange, enemy.whatIsPlayer);
        if (!enemy.playerInSightRange) enemy.SwitchState(enemy.PatrolState);
    }
}
