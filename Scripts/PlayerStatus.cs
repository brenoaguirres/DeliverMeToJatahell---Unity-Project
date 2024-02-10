using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
    public bool isAlive;
    public int loadedAmmo;
    public int reloadAmmo;
    public int maxAmmoOnChamber;
    public int maxAmmoChamberHelper;
    public int CurrentMaxAmmoChamber => maxAmmoOnChamber - loadedAmmo;

    void Awake()
    {
        maxLife = 100;
        life = maxLife;
        speed = 1.5f;
        minDamage = 10;
        maxDamage = 30;
        isAlive = true;
        maxAmmoOnChamber = 10;
        loadedAmmo = maxAmmoOnChamber;
        reloadAmmo = 10;
    }

    public void UpdateReloadAmmo(int ammo)
    {
        reloadAmmo += ammo;
    }

    public void UpdateLoadedAmmo(int ammo)
    {
        loadedAmmo += ammo;
    }

}
