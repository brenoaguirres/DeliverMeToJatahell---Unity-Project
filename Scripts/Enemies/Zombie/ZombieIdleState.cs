using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This State relies on Implementation of animation events calling the following methods: MoveWhileIdle(), StopWhileIdle();

public class ZombieIdleState : State
{
    #region Fields and Properties
    
    // Movement
    private float distanceFromPlayer;
    [SerializeField]
    private float distanceToChase = 6f;
    // Movement
    [SerializeField]
    private float idleSpeed = 0.8f;

    // Positions to roam
    [SerializeField]
    private List<Transform> positionList = new List<Transform>();
    private List<Vector3> targetList = new List<Vector3>();
    private int currentPosition = 0;

    [SerializeField]
    private float stoppingDistance = 0.5f;

    // Component References
    private Transform playerTransform;
    private AnimationsZombie animations;
    private NavMeshAgent navMeshAgent;

    // Return States
    [SerializeField]
    public ZombieChaseState chase;
    
    #endregion

    #region Execution

    public void Awake()
    {
        animations = GetComponentInParent<AnimationsZombie>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    public void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        // Initializes positions for navmesh
        foreach (Transform t in positionList)
        {
            targetList.Add(new Vector3(t.position.x, t.position.y, t.position.z));
        }
        currentPosition = 0;
    }

    public override State RunCurrentState()
    {
        animations.CheckIdle();
        distanceFromPlayer = DistanceFromPlayer();

        if (distanceFromPlayer > distanceToChase)
        {
            Roam();
            animations.CheckIdle();
            return this;
        }
        else
        {
            animations.UncheckIdle();
            navMeshAgent.isStopped = true;
            return chase;
        }
    }

    #endregion

    #region Idle Behaviours

    private void Roam()
    {
        navMeshAgent.speed = idleSpeed;
        navMeshAgent.stoppingDistance = stoppingDistance;
        if (navMeshAgent.enabled)
        {
            if (!navMeshAgent.hasPath || navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                navMeshAgent.SetDestination(GivePosition());
                navMeshAgent.isStopped = true;
            }
        }

    }

    private Vector3 GivePosition()
    {
        //Vector3 localTarget = transform.position;
        Vector3 localTarget = targetList[currentPosition];
        localTarget.y = transform.position.y;

        if (currentPosition >= positionList.Count - 1)
        {
            currentPosition = 0;
        }
        else
        {
            currentPosition++;
        }
        return localTarget;
    }

    // Used for events -- Zombie resumes movement for idle animation
    public void MoveWhileIdle()
    {
        navMeshAgent.isStopped = false;
    }
    // Used for events -- Zombie stops movement for idle animation
    public void StopWhileIdle()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
    }

    public float DistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, playerTransform.position);
    }

    #endregion

}
