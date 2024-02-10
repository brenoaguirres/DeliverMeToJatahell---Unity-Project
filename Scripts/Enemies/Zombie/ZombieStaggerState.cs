using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStaggerState : State
{
    #region Fields and Properties

    // chance to enter stagger state
    [SerializeField][Range(0, 100)]
    private int staggerChance = 35;
    [SerializeField]
    private float staggerTime = 0.3f;

    //Control variables for checking if is still staggering
    private bool isStaggering = false;
    private bool finishedStaggering = false;

    //Reference for animation functions
    private AnimationsZombie animations;

    //References for states
    [SerializeField]
    public ZombieIdleState idle;
    [SerializeField]
    public ZombieChaseState chase;
    [SerializeField]
    public ZombieAttackState attack; 

    public bool isAnimating = false;
    
    #endregion

    #region Execution

    public void Awake()
    {
        animations = GetComponentInParent<AnimationsZombie>();
    }

    public override State RunCurrentState()
    {

        // if zombie is not staggering then it will stagger. After it finishes, the returning state
        //  will be based on the distance of this position to player position; 
        if(!isStaggering)
        {
            StartCoroutine(Stagger());
        }
        if(finishedStaggering)
        {
            //resets finishedStaggering
            finishedStaggering = false;

            float distance = idle.DistanceFromPlayer();
            if(distance > chase.GetDistanceToChase())
            {
                return idle;
            }
            else if (distance <= chase.GetDistanceToChase() && distance > chase.GetStoppingDistance())
            {
                return chase;
            }
            else
            {
                return attack;
            }
        }
        return this;
    }

    public IEnumerator Stagger()
    {
        //blocks repetition of first method and wait staggerTime
        isStaggering = true;
        if (!isAnimating)
        {
            isAnimating = true;
            animations.Stagger();
        }
        yield return new WaitForSeconds(staggerTime);

        //resets isStaggering and blocks allows second method to run
    }
    #endregion

    #region Stagger Behaviours

    public bool HasStagger()
    {
        if(Random.Range(0, 101) <= staggerChance)
        {
            return true;
        }
        
        return false;
    }

    public void FinishedStaggering()
    {
        isAnimating = false;
        isStaggering = false;
        finishedStaggering = true;
    }

    #endregion

}
