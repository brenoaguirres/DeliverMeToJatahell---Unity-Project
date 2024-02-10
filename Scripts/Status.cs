using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : MonoBehaviour
{
    [SerializeField] public float maxLife;
    [SerializeField] public float life;
    [SerializeField] public float speed;
    [SerializeField] public int damage;
    [SerializeField] public int minDamage;
    [SerializeField] public int maxDamage;
}
