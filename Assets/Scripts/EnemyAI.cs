using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform playerPositionTransform;
    
    private NavMeshAgent navMeshAgent;

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

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(walkPointChangeLogic());
    }

    private void Update()
    {        
        Patrolling();
        // ChasePlayer();
    }

    private void Patrolling()
    {
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
        navMeshAgent.destination = playerPositionTransform.position;
    }
}
