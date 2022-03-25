using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{    
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.navMeshAgent.speed = enemy.chaseSpeed;
        enemy.navMeshAgent.angularSpeed = enemy.chaseAngleSpeed;
        enemy.navMeshAgent.acceleration = enemy.chaseAcceleration;

        enemy.enemyAnimator.SetBool(enemy.isRunningHash, true);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.growlLogic();

        enemy.playerInSightRange = Physics.CheckSphere(enemy.navMeshAgent.transform.position, enemy.sightRange, enemy.whatIsPlayer);
        enemy.navMeshAgent.CalculatePath(enemy.playerPositionTransform.position, enemy.path);
        if (enemy.path.status == NavMeshPathStatus.PathComplete)
        {
            enemy.navMeshAgent.destination = enemy.playerPositionTransform.position;
        }
        else
        {
            enemy.enemyAnimator.SetBool(enemy.isRunningHash, false);
            enemy.SwitchState(enemy.PatrolState);
        }
        if (!enemy.playerInSightRange)
        {
            enemy.enemyAnimator.SetBool(enemy.isRunningHash, false);
            enemy.SwitchState(enemy.PatrolState);
        }
    }
}
