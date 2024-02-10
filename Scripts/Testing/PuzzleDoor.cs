using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : Interactable
{
    public Inventory inventory;
    public AudioClip puzzleSFX;
    public AudioClip lockedSFX;

    public override void Interaction()
    {
        if (inventory.CheckInInventory(2))
        {
            AudioController.instance.PlayOneShot(puzzleSFX);
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            AudioController.instance.PlayOneShot(lockedSFX);
        }
    }
}
