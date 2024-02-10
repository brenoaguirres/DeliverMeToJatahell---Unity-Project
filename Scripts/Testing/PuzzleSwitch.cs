using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSwitch : Interactable
{
    public BoxCollider boxCollider;
    public Animator animator;
    [SerializeField]
    public AudioClip puzzleSolved;

    public override void Interaction()
    {
        animator.enabled = false;
        boxCollider.enabled = false;
        AudioController.instance.PlayOneShot(puzzleSolved);
    }
}
