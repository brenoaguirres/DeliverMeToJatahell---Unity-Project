using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnBoss : MonoBehaviour
{
    private float gizmoDistance = 3;
    private float timeForNextSpawn = 0;
    public float TimeBetweenSpawn = 120;
    public GameObject BossPrefab;
    private GUIController GUI;
    private float warningTime = 3;
    public Transform[] SpawnPositions;
    private Transform playerTransform;
    private string playerTag = "Player";
    Vector3 spawnPosition;
    void Start()
    {
        timeForNextSpawn = TimeBetweenSpawn;
        GUI = GameObject.FindObjectOfType(typeof(GUIController)) as GUIController;
        playerTransform = GameObject.FindWithTag(playerTag).transform;
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad > timeForNextSpawn)
        {
            spawnPosition = GenerateSpawnPosition();
            Instantiate(BossPrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(GUI.ShowUITextWithGradient(warningTime, GUI.BossWarningText));
            timeForNextSpawn = Time.timeSinceLevelLoad + TimeBetweenSpawn;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, gizmoDistance);
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 farthestSpawn = Vector3.zero;
        float farthestDistance = 0;
        foreach (Transform t in SpawnPositions)
        {
            float distance = Vector3.Distance(t.position, playerTransform.position);
            if (distance > farthestDistance)
            {
                farthestDistance = distance;
                farthestSpawn = t.position;
            }
            
        }
        return farthestSpawn;
    }
}
