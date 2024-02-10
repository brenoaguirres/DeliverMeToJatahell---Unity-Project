using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This State relies on Implementation of animation events calling the following methods: FinishAttackAnimation();

public class ZombieAttackState : State
{
    #region Fields and Properties

    // Orientation
    private NavMeshAgent navMeshAgent;

    // Animation
    private AnimationsZombie animations;
    private Animator animator;

    // Options for tweaking
    [SerializeField][Range(0, 100)]
    private float attackChance;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float attackRadius = 0.3f;
    [SerializeField]
    private float attackRange = 1.5f;
    [SerializeField]
    private int minDamage;
    [SerializeField]
    private int maxDamage;
    [SerializeField]
    private LayerMask playerLayer;

    // Check if zombie can attack again
    private bool isAttacking = false;
    private float timeToAttackAgain;
    [SerializeField][Tooltip("How much time between zombies tries to attack")]
    private float nextTime = 4f;

    //Test code for control
    private bool hasCalledAnim = false;
    private bool alreadySentDamage = false;

    // Contains reference to player position
    private Transform playerTransform;

    // Returning States
    [SerializeField]
    public ZombieChaseState chase;

    #endregion

    #region Execution

    public void Awake()
    {
        animations = GetComponentInParent<AnimationsZombie>();
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponentInParent<Animator>();

        //Initializes timer with chosen value
        timeToAttackAgain = nextTime;
    }

    public void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    } 

    // Sends AttackState logic to StateMachine
    public override State RunCurrentState()
    {
        // Sets stopping distance from player to attackRange, moves towards player and triggers attack
        navMeshAgent.isStopped = false;
        navMeshAgent.stoppingDistance = attackRange;
        navMeshAgent.SetDestination(playerTransform.position);
        
        if (DistanceFromPlayer() <= attackRange)
        {
            // if timeToAttackAgain <= 0 then roll a chance of attackChance% of allowing an attack
            //  to be made by marking isAttacking as true, thus calling attack function until it tells
            //      the attack animation is over.
            if (timeToAttackAgain <= 0 && !isAttacking)
            {
                int roll = Random.Range(0, 101);

                if (roll <= attackChance)
                {
                    isAttacking = true;
                }
                //Generates random next time to attack again to create unpredictable behaviour
                timeToAttackAgain = Random.Range(0.0f, nextTime);
            }

            if (isAttacking)
            {
                Attack();
            }

            //Check if animation has ended
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
            {
                // isAttacking = false;
                // hasCalledAnim = false;
            }
            
            // updates timer to attack again, decreasing it
            timeToAttackAgain -= Time.deltaTime;

            return this;
        }
        else
        {
            navMeshAgent.isStopped = true;
            return chase;
        }

    }

    #endregion

    #region Attack Behaviours

    private void Attack()
    {
        // Play an attack animation
        if (!hasCalledAnim)
        {
            hasCalledAnim = true;
            animations.Attack();
        }

        // Detect player in radius of attack sphere
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRadius, playerLayer);

        // Damage the player
        if (hitPlayer != null)
        {
            foreach (Collider col in hitPlayer)
            {
                if (!alreadySentDamage)
                {
                    alreadySentDamage = true;
                    if (col.tag == "Player")
                    {
                        int damage = Random.Range(minDamage, maxDamage);
                        col.GetComponent<PlayerController>().ReceiveDamage(damage);
                    }
                }
            }
        }
    }

    //Draw Attack Sphere Gizmo on zombie's hand
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    // Returns the distance between enemy and player
    private float DistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, playerTransform.position);
    }

    // Test code
    public void FinishAttackAnimation()
    {
        this.isAttacking = false;
        this.hasCalledAnim = false;
        this.alreadySentDamage = false;
    }

    #endregion
}
