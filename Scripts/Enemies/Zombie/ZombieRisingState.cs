using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRisingState : State
{
    // Tweaks
    [SerializeField]
    private float risingTime = 1f;

    // Animations
    private AnimationsZombie animations;

    //Control Variables
    private bool isRising = false;
    private bool finishedRising = false;

    //States
    [SerializeField]
    private ZombieIdleState idle;

    public void Awake()
    {
        animations = GetComponentInParent<AnimationsZombie>();
    }

    public override State RunCurrentState()
    {
        if(!isRising)
        {
            StartCoroutine(Rising());
        }
        if(finishedRising)
        {
            //resets finishedStaggering
            finishedRising = false;
            return idle;
        }
        return this;
    }

    public IEnumerator Rising()
    {  
        isRising = true;
        animations.Rise();

        yield return new WaitForSeconds(risingTime);
    }

    // Called on animation event
    public void FinishedRising()
    {
        isRising = false;
        finishedRising = true;
    }
}
