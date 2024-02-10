using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    public virtual void Attack()
    {
        switch (weaponManager.currentWeapon)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
