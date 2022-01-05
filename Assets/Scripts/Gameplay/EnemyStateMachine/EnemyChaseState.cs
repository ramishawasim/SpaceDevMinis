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
        enemy.playerInSightRange = Physics.CheckSphere(enemy.navMeshAgent.transform.position, enemy.sightRange, enemy.whatIsPlayer);
        enemy.navMeshAgent.CalculatePath(enemy.playerPositionTransform.position, enemy.path);
        if (enemy.path.status == NavMeshPathStatus.PathComplete)
        {
            enemy.navMeshAgent.destination = enemy.playerPositionTransform.position;
        }
        else
        {
            enemy.SwitchState(enemy.PatrolState);
        }
        if (!enemy.playerInSightRange) enemy.SwitchState(enemy.PatrolState);
    }
}
