using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAnimationEvents : MonoBehaviour
{
    // This script passes calls of animation events of animator to the states on child objects
    private ZombieIdleState idleState;
    private ZombieAttackState attackState;
    private ZombieFakeDeathState fakeDeathState;
    private ZombieRisingState risingState;
    private ZombieStaggerState staggerState;
    private void Awake()
    {
        idleState = GetComponentInChildren<ZombieIdleState>();
        attackState = GetComponentInChildren<ZombieAttackState>();
        fakeDeathState =  GetComponentInChildren<ZombieFakeDeathState>();
        risingState = GetComponentInChildren<ZombieRisingState>();
        staggerState = GetComponentInChildren<ZombieStaggerState>();
    }

    public void EnableIdleMovement()
    {
        idleState.MoveWhileIdle();
    }

    public void DisableIdleMovement()
    {
        idleState.StopWhileIdle();
    }

    public void FinishAttack()
    {
        attackState.FinishAttackAnimation();
    }

    public void FinishFaking()
    {
        fakeDeathState.FinishedFaking();
    }

    public void FinishRising()
    {
        risingState.FinishedRising();
    }

    public void FinishStagger()
    {
        staggerState.FinishedStaggering();
    }
}
