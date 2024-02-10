using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField][Range(0, 4)]
    private int weaponType;
    private WeaponManager weaponManager;
    private CharacterAnimator animator;

    // Variables to pass on to GunController
    [SerializeField]
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private int weaponDamage = 10;
    [SerializeField]
    private int weaponRange = 100;
    private void Awake() 
    {
        weaponManager = GetComponentInParent<WeaponManager>();
        animator = GetComponentInParent<CharacterAnimator>();
    }

    private void OnEnable() 
    {
        CheckForWeaponManager();
        weaponManager.SetCurrentWeapon(weaponType, this);
        animator.OutIdle();

        //Sends info to gunController about this gun
        if (this.muzzleFlash != null)
        {
            weaponManager.gunController.muzzleFlash = this.muzzleFlash;
        }
        weaponManager.gunController.damage = weaponDamage;
        weaponManager.gunController.range = weaponRange;
    }   

    private void OnDisable() 
    {
        weaponManager.SetCurrentWeapon(0, null);
        animator.OutIdle();
    }

    public void EquipThis()
    {
        
        CheckForWeaponManager();
        if (weaponManager.lastWeapon != null)
        {
            weaponManager.lastWeapon.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(true);
    }

    public void CheckForWeaponManager()
    {
        if (weaponManager == null)
        {
            weaponManager = GetComponentInParent<WeaponManager>();
        }
    }
}
