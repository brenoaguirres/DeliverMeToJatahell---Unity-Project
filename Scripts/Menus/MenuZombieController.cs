using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuZombieController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    public AudioClip Moan1;
    public AudioClip Moan2;
    public AudioClip Moan3;

    private float countdownDestroy = 15;
    private float lastMoan;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        AnimationMovement();
        StartCoroutine(DestroyZombie());
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + transform.forward.normalized * 1.2f * Time.deltaTime);
        countdownDestroy -= Time.deltaTime;
        lastMoan -= Time.deltaTime;
        if (countdownDestroy > 5)
        {
            ZombieMoan();
        }
    }

    public void AnimationMovement()
    {
        animator.SetBool("isWalking", true);
    }

    public IEnumerator DestroyZombie()
    {

        yield return new WaitForSeconds(countdownDestroy);
        Destroy(gameObject);
    }

    public void ZombieMoan()
    {
        if (lastMoan <= 0)
        {
            float chance = Random.value;
            if (chance <= 0.35)
            {
                chance = Random.Range(0, 3);
                switch (chance)
                {
                    case 0:
                        audioSource.PlayOneShot(Moan1);
                        lastMoan = 5;
                        break;
                    case 1:
                        audioSource.PlayOneShot(Moan2);
                        lastMoan = 5;
                        break;
                    case 2:
                        audioSource.PlayOneShot(Moan3);
                        lastMoan = 5;
                        break;
                }
            }
        }
    }
}
