using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages methods to animate a generic Character

public class CharacterAnimator : MonoBehaviour
{
    #region Fields and Properties
    private Animator animator;
    #endregion

    #region Initialization
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    #endregion

    #region Methods
    public void Attack(bool estado)
    {
        animator.SetBool("Attack", estado);
    }

    public void Movement(Vector3 direction)
    {
        animator.SetFloat("Move", direction.magnitude);
    }

    public void Run(bool toggle)
    {
        animator.SetBool("Run", toggle);
    }

    public void Shoot()
    {
        animator.SetTrigger("Attack");
    }

    public void Death()
    {
        animator.SetTrigger("Killed");
    }

    public void Aim(bool toggle)
    {
        animator.SetBool("Aim", toggle);
    }

    public bool GetAim()
    {
        return animator.GetBool("Aim");
    }

    public void OutIdle()
    {
        animator.SetTrigger("OtherAnimHasPlayed");
    }
    #endregion
}