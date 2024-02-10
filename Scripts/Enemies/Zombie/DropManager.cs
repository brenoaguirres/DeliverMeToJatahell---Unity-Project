using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> drops = new List<ItemData>();

    public void RaffleDrops()
    {
        if (drops != null)
        {
            int drop = Random.Range(0, drops.Count);
            Instantiate(drops[drop].prefab, transform.position, Quaternion.identity);
        }
    }
}
