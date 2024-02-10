using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : Interactable
{
    public GameObject zombie;

    public GameObject spawn;

    public override void Interaction()
    {
        Instantiate(zombie, spawn.transform.position, Quaternion.identity);
    }
}
