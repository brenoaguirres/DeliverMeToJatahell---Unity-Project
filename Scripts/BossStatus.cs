using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds mini-boss stats

public class BossStatus : Status
{
    void Start()
    {
        maxLife = 300;
        life = maxLife;
        speed = 4.5f;
        minDamage = 50;
        maxDamage = 80;
    }
}
