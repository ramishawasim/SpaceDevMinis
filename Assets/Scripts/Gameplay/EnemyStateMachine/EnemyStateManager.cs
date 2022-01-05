using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{

    EnemyBaseState currentState;
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyChaseState ChaseState = new EnemyChaseState();

    [SerializeField]
    public Transform playerPositionTransform;

    public NavMeshAgent navMeshAgent;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsNotPlayer;

    // Speeds

    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 5.5f;
    public float patrolAngleSpeed = 250f;
    public float chaseAngleSpeed = 500;

    // Patrol Parameters

    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange = 1f;
    public float walkPointMinimum = 2f;
    public float pauseTimeMin = 1f;
    public float pauseTimeMax = 2f;
    private float pauseTime;

    // "Update()"

    public float sightRange;
    public bool playerInSightRange;
    public float stateUpdateTime = 0.2f;
    public NavMeshPath path;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        walkPoint = transform.position;
        currentState = PatrolState;
        currentState.EnterState(this);
        StartCoroutine(walkPointChangeLogic());
        StartCoroutine(stateUpdate());
    }

    IEnumerator stateUpdate()
    {
        for (; ; )
        {
            currentState.UpdateState(this);
            yield return new WaitForSeconds(stateUpdateTime);
        }
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    IEnumerator walkPointChangeLogic()
    {
        for (; ; )
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

        walkPoint = new Vector3(navMeshAgent.transform.position.x + randomX, navMeshAgent.transform.position.y, navMeshAgent.transform.position.z + randomZ);
        walkPointSet = true;
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            Debug.Log("KILL");
        }
    }

    private void OnDrawGizmosSelected()
    {
        //see range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
