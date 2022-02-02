using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.navMeshAgent.speed = enemy.patrolSpeed;
        enemy.navMeshAgent.angularSpeed = enemy.patrolAngleSpeed;
        enemy.navMeshAgent.acceleration = enemy.patrolAcceleration;

        enemy.enemyAnimator.SetBool(enemy.isWalkingHash, true);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.navMeshAgent.velocity.magnitude < 0.33f)
        {
            enemy.enemyAnimator.SetBool(enemy.isWalkingHash, false);
        }
        else
        {
            enemy.enemyAnimator.SetBool(enemy.isWalkingHash, true);
        }
        enemy.navMeshAgent.CalculatePath(enemy.walkPoint, enemy.path);
        if (enemy.path.status == NavMeshPathStatus.PathComplete)
        {
            enemy.navMeshAgent.destination = enemy.walkPoint;
        }
        enemy.playerInSightRange = Physics.CheckSphere(enemy.navMeshAgent.transform.position, enemy.sightRange, enemy.whatIsPlayer);
        enemy.navMeshAgent.CalculatePath(enemy.playerPositionTransform.position, enemy.path);
        if (enemy.playerInSightRange && enemy.path.status == NavMeshPathStatus.PathComplete) enemy.SwitchState(enemy.ChaseState);
    }
}
