using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    public GameObject Zombie;
    [HideInInspector]
    public float Stopwatch = 0;
    [HideInInspector]
    public float RespawnTime = 1;

    public LayerMask EnemyLayer;
    private float generationDistance = 3;

    private GameObject player;
    private bool playerOutOfRange;
    private float playerMinDistance = 40;

    private float maxZombiesAlivePerGen = 2;
    private float aliveZombies;
    private float zombiesLeftInGen = 4;

    private float timeForDifficultyScale = 60;
    private float difficultyScaleCounter = 0;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        for (int i = 0; i < maxZombiesAlivePerGen; i++)
        {
            StartCoroutine(GenerateZombie());
        }
    }

    void Update()
    {
        CheckZombiesLeft();

        playerOutOfRange = Vector3.Distance(transform.position, player.transform.position) > playerMinDistance;
        if (playerOutOfRange && aliveZombies < maxZombiesAlivePerGen)
        {
            Stopwatch += Time.deltaTime;
            if (Stopwatch >= RespawnTime)
            {
                StartCoroutine(GenerateZombie());
                Stopwatch = 0;
            }
        }
    }

    Vector3 RandomizePosition()
    {
        Vector3 position = Random.insideUnitSphere * generationDistance;
        position += transform.position;
        position.y = 0.5f;

        return position;
    }
    
    IEnumerator GenerateZombie()
    {
        Vector3 spawnPosition = RandomizePosition();
        Collider[] colliderCollection = Physics.OverlapSphere(spawnPosition, 1, EnemyLayer);

        while (colliderCollection.Length > 0)
        {
            spawnPosition = RandomizePosition();
            colliderCollection = Physics.OverlapSphere(spawnPosition, 1, EnemyLayer);
            yield return null;
        }
        ZombieController zombieCon = Instantiate(Zombie, spawnPosition, transform.rotation)
            .GetComponent<ZombieController>();
        zombieCon.MyGen = this;
        aliveZombies++;
    }

    public void DiminishAliveZombies()
    {
        zombiesLeftInGen--;
        aliveZombies--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, generationDistance);
    }

    private void CheckZombiesLeft()
    {
        if (zombiesLeftInGen == 0)
        {
            Destroy(gameObject);
        }
    }

    private void DifficultyScale()
    {
        if(Time.timeSinceLevelLoad > difficultyScaleCounter)
        {
            difficultyScaleCounter = Time.timeSinceLevelLoad + timeForDifficultyScale;
            maxZombiesAlivePerGen++;
        }
    }
}
