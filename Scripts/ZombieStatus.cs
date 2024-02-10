using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatus : Status
{
    void Start()
    {
        maxLife = 100;
        life = maxLife;
        speed = 2;
        minDamage = 20;
        maxDamage = 30;
    }
}
