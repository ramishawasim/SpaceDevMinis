using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform playerPositionTransform;
    
    private NavMeshAgent navMeshAgent;
    public LayerMask whatIsPlayer;

    public float patrolSpeed;
    public float chaseSpeed;
    public float patrolAngleSpeed;
    public float chaseAngleSpeed;

    // Patrolling

    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    public float walkPointMinimum;
    public float pauseTimeMin;
    public float pauseTimeMax;
    private float pauseTime;

    // States

    public float sightRange;
    private bool playerInSightRange;
    public float stateUpdateTime;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.angularSpeed = patrolAngleSpeed;
        StartCoroutine(walkPointChangeLogic());
        StartCoroutine(stateUpdate());
    }

    IEnumerator stateUpdate()
    {
        for (; ; )
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

            if (!playerInSightRange) Patrolling();
            else ChasePlayer();
            yield return new WaitForSeconds(stateUpdateTime);
        }
    }

    private void Patrolling()
    {
        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.angularSpeed = patrolAngleSpeed;
        navMeshAgent.destination = walkPoint;
    }

    IEnumerator walkPointChangeLogic()
    {
        for (;;) 
        {
            if (!walkPointSet)
            {
                SearchWalkPoint();
            }
            else
            {
                walkPointSet = false;
            }

            pauseTime = Random.Range(pauseTimeMin, pauseTimeMax);
            yield return new WaitForSeconds(pauseTime);
        }
    }

    public void SearchWalkPoint()
    {
        // Calc random point in range
        float randomZ = Random.Range(-walkPointRange - walkPointMinimum, walkPointRange + walkPointMinimum);
        float randomX = Random.Range(-walkPointRange - walkPointMinimum, walkPointRange + walkPointMinimum);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        walkPointSet = true;
    }

    private void ChasePlayer()
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.angularSpeed = chaseAngleSpeed;
        navMeshAgent.destination = playerPositionTransform.position;
    }
    private void OnDrawGizmosSelected()
    {
        //see range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
