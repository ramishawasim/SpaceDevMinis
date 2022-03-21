using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class EnemyStateManager : MonoBehaviour
{

    EnemyBaseState currentState;
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public Animator enemyAnimator;

    public int isWalkingHash;
    public int isRunningHash;
    public int isAttackingHash;

    public Transform playerPositionTransform;

    public NavMeshAgent navMeshAgent;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsNotPlayer;

    // Speeds

    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 5.5f;
    public float patrolAngleSpeed = 250f;
    public float chaseAngleSpeed = 500;

    public float chaseAcceleration = 20f;
    public float patrolAcceleration = 5f;

    // Patrol Parameters

    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange = 1f;
    public float walkPointMinimum = 2f;
    public float pauseTimeMin = 1f;
    public float pauseTimeMax = 2f;
    private float pauseTime;

    // Fake "Update"

    public float sightRange;
    public bool playerInSightRange;
    public float stateUpdateTime = 0.2f;
    public NavMeshPath path;

    // player object
    GameObject player;
    SkinnedMeshRenderer skinnedMesh;
    public VisualEffect blood;


    void Start()
    {
        enemyAnimator = GetComponent<Animator>(); 
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isAttackingHash = Animator.StringToHash("isAttacking");

        player = GameObject.Find("Player");
        playerPositionTransform = player.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        walkPoint = transform.position;
        currentState = PatrolState;
        currentState.EnterState(this);
        StartCoroutine(walkPointChangeLogic());
        StartCoroutine(stateUpdate());

        skinnedMesh = player.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>();
        blood = player.transform.GetChild(3).gameObject.GetComponent<VisualEffect>();
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
            StartCoroutine(killCoroutine());
        }
    }

    IEnumerator killCoroutine()
    {
        Debug.Log("KILL");
        // enemyAnimator.SetBool(isAttackingHash, true);
        // navMeshAgent.isStopped = true;
        skinnedMesh.enabled = false;
        blood.Play();
        player.GetComponent<PlayerAndAnimationControllerV2>().onDeath();
        yield return new WaitForSeconds(2);
        // enemyAnimator.SetBool(isAttackingHash, false);
        // navMeshAgent.isStopped = false;
    }

    private void OnDrawGizmosSelected()
    {
        //see range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
