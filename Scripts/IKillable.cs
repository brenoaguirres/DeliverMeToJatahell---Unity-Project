using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    void ReceiveDamage(int damage);
    void CallBloodVFX(Vector3 position, Quaternion rotation);
    void Death();
}
