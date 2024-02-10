using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : Item
{
    private int givenAmmo = 10;
    private new void Awake()
    {
        InitializeInteractable();
    }

    public override void Interaction()
    {
        Pickup();
    }

    public override bool Use()
    {
        playerController.AddAmmo(givenAmmo);
        return true;
    }
}
