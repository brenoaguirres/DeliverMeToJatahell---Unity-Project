using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Interactable
{
    public string ItemID { get; set; }
    public string Title { get; set; }
    public string DocumentText { get; set; }

    public CollectibleData colData;

    public override void Interaction()
    {
        Pickup();
        Destroy(gameObject);
    }

    public void Pickup()
    {
        bool wasPickedUp = playerJournal.AddItem(colData);
        if (wasPickedUp)
        {
            playerController.canInteract = false;
            Destroy(gameObject);
        }
    }
}
