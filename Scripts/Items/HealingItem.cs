using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Item
{
    public int HealQuantity;

    public override void Interaction()
    {
        Pickup();
    }

    public override bool Use()
    {
        playerController.Heal(HealQuantity);
        return true;
    }
}
