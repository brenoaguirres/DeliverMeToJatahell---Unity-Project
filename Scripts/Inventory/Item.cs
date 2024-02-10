using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public string ItemID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }

    public ItemData itemData;

    public override void Interaction()
    {
        Pickup();
        Destroy(gameObject);
    }

    public void Pickup()
    {
        bool wasPickedUp = playerInventory.AddItem(itemData);
        if (wasPickedUp)
        {
            playerController.canInteract = false;
            Destroy(gameObject);
        }
    }

    public virtual bool Use()
    {
        throw new NotImplementedException();
    }
}
