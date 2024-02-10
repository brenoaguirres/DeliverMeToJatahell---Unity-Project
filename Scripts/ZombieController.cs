using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour, IKillable
{
    public GameObject Player;
    Vector3 direction;
    float stopDistance = 1f;
    float distanceFromPlayer;

    private CharacterMovement characterMovement;
    private CharacterAnimator characterAnimator;
    private ZombieStatus status;

    public AudioClip DmgSound;
    public AudioClip DeathSound;

    private Vector3 randomPosition;
    private float roamStopwatch;
    private float timeBetweenRandomPos = 4;
    public LayerMask ScenarioLayer;

    public GameObject MedkitPrefab_S;
    public GameObject MedkitPrefab_M;
    public GameObject AmmoBox_Prefab;
    private float MkS_Spawn_Chance = 0.025f;
    private float MkM_Spawn_Chance = 0.01f;
    private float AmmoBox_Drop_Chance = 0.1f;

    private GUIController GUI;

    public SpawnZombie MyGen;

    public GameObject Blood_VFX;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        GUI = GameObject.Find("Canvas").GetComponent<GUIController>();

        characterMovement = GetComponent<CharacterMovement>();
        characterAnimator = GetComponent<CharacterAnimator>();
        status = GetComponent<ZombieStatus>();

        direction = Player.transform.position - transform.position;

        ZombieRandomizer();
    }

    private void FixedUpdate()
    {

        distanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);

        characterMovement.Rotate(direction);
        characterAnimator.Movement(direction);

        if (distanceFromPlayer > 15)
        {
            Roam();
        }
        else if (distanceFromPlayer > stopDistance)
        {
            direction = Player.transform.position - transform.position;
            characterMovement.Movement(direction.normalized, status.speed);

            characterAnimator.Attack(false);
            Debug.Log("Attack is false");
        }
        else
        {
            Debug.Log("Attack is true");
            direction = Player.transform.position - transform.position;
            characterAnimator.Attack(true);
        }
    }

    public void AttackPlayer()
    {
        status.damage = Random.Range(status.minDamage, status.maxDamage);
        Player.GetComponent<PlayerController>().ReceiveDamage(status.damage);
    }

    void ZombieRandomizer()
    {
        int ZombieTypeRandomizer = Random.Range(1, transform.childCount);
        transform.GetChild(ZombieTypeRandomizer).gameObject.SetActive(true);
    }

    public void ReceiveDamage(int damage)
    {
        status.life -= damage;
        AudioController.instance.PlayOneShot(DmgSound);
        CallBloodVFX(transform.position, Quaternion.LookRotation(-transform.forward));
        if (status.life <= 0)
        {
            Death();
        }
    }

    public void CallBloodVFX (Vector3 position, Quaternion rot)
    {
        Instantiate(Blood_VFX, position, rot);
    }

    public void Death()
    {
        AudioController.instance.PlayOneShot(DeathSound);
        RaffleDrops();

        characterAnimator.Death();
        this.enabled = false;
        characterMovement.Death();
        Destroy(gameObject, 1);

        MyGen.DiminishAliveZombies();
        GUI.UpdateKilledZombies();
    }

    void Roam()
    {
        roamStopwatch -= Time.deltaTime;

        if (roamStopwatch <= 0)
        {
            randomPosition = RandomizePosition();
            roamStopwatch = timeBetweenRandomPos + Random.Range(-1f, 1f);
        }

        bool isCloseEnough = Vector3.Distance
            (transform.position, randomPosition) <= 0.05;
        
        if (isCloseEnough == false)
        {
            direction = randomPosition - transform.position;
            characterMovement.Movement(direction, status.speed);
        }
    }

    void RaffleDrops()
    {
        if (RaffleMedkits(MkS_Spawn_Chance))
        {
            return;
        }
        if (RaffleMedkits(MkM_Spawn_Chance))
        {
            return;
        }
        if (Random.value <= AmmoBox_Drop_Chance)
        {
            Instantiate(AmmoBox_Prefab, 
                transform.position + new Vector3(0.5f, 0, 0.5f), Quaternion.identity);
            return;
        }
    }

    bool RaffleMedkits(float chance)
    {
        if (chance == MkS_Spawn_Chance)
        {
            if(Random.value <= chance)
            {
                Instantiate(MedkitPrefab_S, 
                    transform.position, Quaternion.identity);
                return true;
            }
        }
        else if (chance == MkM_Spawn_Chance)
        {
            if (Random.value <= chance)
            {
                Instantiate(MedkitPrefab_M,
                    transform.position, Quaternion.identity);
                return true;
            }
        }
        return false;
    }

    Vector3 RandomizePosition()
    {
        Vector3 position = Random.insideUnitSphere * 10;
        position += transform.position;
        position.y = transform.position.y;

        return position;
    }
}