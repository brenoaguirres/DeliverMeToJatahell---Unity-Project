using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    
    private GunController gun;
    private GameObject currentWeapon;

    private void Awake() 
    {
        gun = GetComponent<GunController>();    
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MakeAttack ()
    {
        if (currentWeapon.tag == "Handgun")
        {
            gun.Shoot();
        }
        else if (currentWeapon.tag == "Melee")
        {
            
        }
    }
}
