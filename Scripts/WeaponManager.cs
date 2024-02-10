using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Animator animator;
    [Range(0, 4)]
    [HideInInspector]
    public int currentWeapon;
    [HideInInspector]
    public Weapon lastWeapon = null;
    [HideInInspector]
    public GunController gunController;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        gunController = GetComponent<GunController>();
    }

    public void SetCurrentWeapon(int weaponCode, Weapon weapon)
    {
        currentWeapon = weaponCode;
        animator.SetInteger("WeaponType", currentWeapon);
        lastWeapon = weapon;
    }
}
