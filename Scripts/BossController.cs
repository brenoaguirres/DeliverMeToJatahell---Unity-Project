using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Handles logic and controlls Mini-Boss (Stalker) Behaviour
// Stalker is a kind of enemy that searches for the player when is spawned

public class BossController : MonoBehaviour, IKillable
{
    #region Fields and Properties
    //Movement
    private Transform player;
    private NavMeshAgent agent;
    private CharacterMovement bossMovement;

    //Status
    private BossStatus bossStatus;
    
    //Animation
    private CharacterAnimator bossAnimator;

    //Lifebar
    public Slider bossLifebar;
    public Image sliderImage;
    public Color MaxHPHue, MinHpHue;

    //Audio
    public AudioClip DmgSFX, DeathSFX;
    
    //Drops
    public GameObject MedkitPrefab;

    //VFX
    public GameObject Blood_VFX;
    #endregion

    #region Execution
    void Start()
    {
        InitializeComponents();
        InitializeBossHP();
        UpdateUI();
    }

    void Update()
    {
        Behaviour();
    }
    #endregion

    #region Behaviour

    public void Behaviour()
    {
        agent.speed = bossStatus.speed;
        agent.stoppingDistance = 5;

        if (agent.enabled)
        {
            agent.SetDestination(player.position);
            bossAnimator.Movement(agent.velocity);

            if (agent.hasPath)
            {
                bool iAmCloseEnough = agent.remainingDistance <= agent.stoppingDistance;
                if (iAmCloseEnough)
                {
                    bossAnimator.Attack(true);
                    Vector3 direction = player.position - transform.position;
                    bossMovement.Rotate(direction);
                }
                else
                {
                    bossAnimator.Attack(false);
                    agent.SetDestination(player.position);
                    bossAnimator.Movement(agent.velocity);
                }
            }
        }
    }

    public void AttackPlayer()
    {
        int damage = Random.Range(bossStatus.minDamage, bossStatus.maxDamage);
        player.GetComponent<PlayerController>().ReceiveDamage(damage);
    }

    public void ReceiveDamage(int damage)
    {
        AudioController.instance.PlayOneShot(DmgSFX);
        bossStatus.life -= damage;
        UpdateUI();
        if(bossStatus.life <= 0)
        {
            Death();
        }
    }

    public void CallBloodVFX(Vector3 position, Quaternion rot)
    {
        Instantiate(Blood_VFX, position, rot);
    }

    public void Death()
    {
        Instantiate(MedkitPrefab, transform.position, Quaternion.identity);
        AudioController.instance.PlayOneShot(DeathSFX);
        agent.enabled = false;
        bossAnimator.Death();
        bossMovement.Death();
        this.enabled = true;
        Destroy(gameObject, 2);
    }

    public void UpdateUI()
    {
        bossLifebar.value = bossStatus.life;
        float remainingLife = (float)bossStatus.life / (float)bossStatus.maxLife;
        Color lifeColor = Color.Lerp(MinHpHue, MaxHPHue, remainingLife);
        sliderImage.color = lifeColor;
    }
    #endregion

    #region Initialization
    public void InitializeBossHP()
    {
        bossStatus.maxLife = 500;
        bossStatus.life = bossStatus.maxLife;
        bossLifebar.maxValue = bossStatus.maxLife;
    }

    public void InitializeComponents()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        bossStatus = GetComponent<BossStatus>();
        bossAnimator = GetComponent<CharacterAnimator>();
        bossMovement = GetComponent<CharacterMovement>();
        bossLifebar = GetComponentInChildren<Slider>();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override string ToString()
    {
        return base.ToString();
    }
    #endregion
}