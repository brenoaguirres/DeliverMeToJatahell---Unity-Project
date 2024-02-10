using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This State relies on Implementation of animation events calling the following methods: MoveWhileIdle(), StopWhileIdle();

public class ZombieChaseState : State
{
    #region Fields and Properties

    // Movement
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float stoppingDistance = 1.5f;

    [SerializeField]
    private float distanceToChase = 6f;

    // Component References
    private Transform playerTransform;

    private NavMeshAgent navMeshAgent;

    private AnimationsZombie animations;

    private Rigidbody myRigidbody;

    // Return States
    [SerializeField]
    public ZombieIdleState idle;
    [SerializeField]
    public ZombieAttackState attack;

    #endregion
    
    #region Execution

    private void Awake() 
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        animations = GetComponentInParent<AnimationsZombie>();
        myRigidbody = GetComponentInParent<Rigidbody>();
    }

    private void Start() 
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public override State RunCurrentState()
    {
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = stoppingDistance;

        if (navMeshAgent.enabled)
        {
            if (DistanceFromPlayer() <= distanceToChase)
            {
                navMeshAgent.SetDestination(playerTransform.position);
                animations.Walk(navMeshAgent.velocity);
                animations.UncheckIdle();

                if (navMeshAgent.hasPath)
                {
                    if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                    {
                        navMeshAgent.isStopped = false;
                        return attack;
                    }
                }
                
                return this;
            }
        }

        return idle;
    }

    #endregion

    #region Movement Behaviours

    private float DistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, playerTransform.position);
    }

    public float GetDistanceToChase()
    {
        return distanceToChase;
    }

    public float GetStoppingDistance()
    {
        return stoppingDistance;
    }

    #endregion

}
