using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : InteractableRefactored
{
    #region Fields and Properties

    private Animator animator;
    private bool isDoorOpen = false;
    [SerializeField]
    private bool isDoorUnlocked = true;
    [SerializeField]
    private List<ItemData> keys = new List<ItemData>();
    private bool check = false;

    #endregion

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }

    #region Methods

    public override void Interact()
    {
        if (hasInteracted)
        {
            DoorInteraction();
        }
    }

    public void PlayAnimation()
    {
        if (!isDoorOpen)
        {
            Debug.Log("Opening Door");
            animator.SetBool("Opening", true);
            animator.SetBool("Closing", false);
            isDoorOpen = true;
        }
        else
        {
            animator.SetBool("Closing", true);
            animator.SetBool("Opening", false);
            isDoorOpen = false;
        }
    }

    public void DoorInteraction()
    {
        Debug.Log("Played interact ");
        if (playerInventory == null)
        {
            playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        }
        if (isDoorUnlocked)
        {
            Debug.Log("Door unlocked");
            PlayAnimation();
        }
        else
        {
            if (playerInventory != null)
            {
                foreach (ItemData k in keys)
                {
                    if (playerInventory.CheckInInventory(k.item_id))
                    {
                        check = true;
                    }
                    else
                    {
                        check = false;
                    }
                }
            }
            if (check)
            {
                isDoorUnlocked = true;
            }
        }

        hasInteracted = false;
    }

    #endregion
}
