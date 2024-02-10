using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDeadState : State
{
    #region Fields and Properties

    private float timeToDie = 1.5f;

    // Components
    private AnimationsZombie animations;
    private NavMeshAgent navMeshAgent;
    private DropManager dropManager;

    //Control Variables
    private bool deathCalled = false;

    //Uncheck Player Aim
    private PlayerController playerController;

    #endregion

    #region Execution

    private void Start() 
    {
        animations = GetComponentInParent<AnimationsZombie>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        dropManager = GetComponentInParent<DropManager>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public override State RunCurrentState()
    {
        if (!deathCalled)
        {
            deathCalled = true;
            
            navMeshAgent.isStopped = true;
            animations.Death();
            dropManager.RaffleDrops();
            playerController.DeactivateTargetLock();
            GameObject.Destroy(GetComponentInParent<Animator>().gameObject, timeToDie);
        }
        return null;
    }

    #endregion

}
