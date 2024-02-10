using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponentZombie : MonoBehaviour
{
    #region Fields and Properties

    // used for hp control
    private float health;
    [SerializeField]
    private float maxHealth = 100;

    // Minimum health to have possibility to trigger FakeDeath State
    private float minHealthForFakeDeath = 0.2f;

    // Reference for States
    [SerializeField]
    private ZombieStaggerState stagger;
    [SerializeField]
    private ZombieDeadState dead;
    [SerializeField]
    private ZombieFakeDeathState fakeDeath;
    [SerializeField]
    private StateManager stateManager;

    // Checks if zombie can stagger or fakeDeath
    private bool canAnimate;

    #endregion

    #region Execution

    // sets health to maxHealth at the start
    private void Start() 
    {
        health = maxHealth;
    }

    #endregion

    #region Health Behaviours

    // Removes player health and tries to call stagger, calls Death if health is below 0
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= maxHealth * minHealthForFakeDeath)
        {
            if (fakeDeath.HasFakeDeath())
            {
                if (!(stateManager.GetState() is ZombieFakeDeathState || 
                        stateManager.GetState() is ZombieRisingState))
                            {
                                stateManager.SetState(fakeDeath);
                            }
            }
        }

        if (stagger.HasStagger())
        {
            if (!(stateManager.GetState() is ZombieFakeDeathState || 
                    stateManager.GetState() is ZombieRisingState))
                        {
                            stateManager.SetState(stagger);
                        }
        }
        
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        stateManager.SetState(dead);
    }

    #endregion


}
