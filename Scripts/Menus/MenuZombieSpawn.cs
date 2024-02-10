using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuZombieSpawn : MonoBehaviour
{
    private float MaxSpawnTime = 30;
    private float timeToSpawn = 10;
    public GameObject MenuZombie;

    void Update()
    {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0)
        {
            timeToSpawn = MaxSpawnTime;
            Vector3 rotation = new Vector3(90, 0, 0);
            Quaternion rot = Quaternion.LookRotation(rotation);
            Instantiate(MenuZombie, transform.position,
                rot);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
}
