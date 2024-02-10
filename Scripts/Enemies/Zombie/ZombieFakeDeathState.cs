using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieFakeDeathState : State
{

    #region Fields and Properties
    [SerializeField][Range(0, 100)]
    private float fakeDeathChance = 35;
    [SerializeField]
    private float fakingTime = 3f;

    // control for no repetition
    private bool isFaking = false;
    private bool finishedFaking = false;

    //Uncheck Player Aim
    private PlayerController playerController;

    //Components
    private AnimationsZombie animations;
    private NavMeshAgent navMeshAgent;

    //States
    [SerializeField]
    private ZombieRisingState rising;

    #endregion 

    #region Execution
    
    public void Awake()
    {
        animations = GetComponentInParent<AnimationsZombie>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    private void Start() 
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public override State RunCurrentState()
    {
        // Stops zombie movement and deactivate player's target lock.
        navMeshAgent.isStopped = true;
        playerController.DeactivateTargetLock();

        if(!isFaking)
        {
            StartCoroutine(FakeDeath());
        }
        if(finishedFaking)
        {
            isFaking = false;
            finishedFaking = false;

            return rising;
        }
        return this;
    }

    #endregion

    #region FakeDeath Behaviours

    public bool HasFakeDeath()
    {
        if(Random.Range(0, 101) <= fakeDeathChance)
        {
            return true;
        }
        
        return false;
    }
    
    public IEnumerator FakeDeath()
    {
        isFaking = true;
        
        animations.FakeDeath();

        yield return new WaitForSeconds(fakingTime);

        finishedFaking = true;
    }

    // Called as animation event
    public void FinishedFaking()
    {
        // isFaking = false;
        // finishedFaking = true;
    }

    #endregion
}
