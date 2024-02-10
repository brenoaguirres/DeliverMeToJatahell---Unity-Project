using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsPlayer : MonoBehaviour
{
    #region Fields and Properties

    private Animator animator;

    #endregion

    #region Execution

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    #endregion

    #region Animation Calls

    public void Walk(Vector3 direction)
    {
        animator.SetFloat("Walk", direction.magnitude);
    }

    public void Run(Vector3 direction)
    {
        animator.SetFloat("Run", direction.magnitude);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void CheckIdle()
    {
        animator.SetBool("Idle", true);
    }

    public void UncheckIdle()
    {
        animator.SetBool("Idle", false);
    }

    public void Stagger()
    {
        animator.SetTrigger("Stagger");
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void Reload()
    {
        animator.SetTrigger("Reload");
    }

    public void Stomp()
    {
        animator.SetTrigger("Stomp");
    }

    #endregion
}
