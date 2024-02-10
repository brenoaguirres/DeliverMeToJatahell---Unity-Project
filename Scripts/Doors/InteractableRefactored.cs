using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableRefactored : MonoBehaviour
{
    public bool hasInteracted = false;
    public Inventory playerInventory;
    public abstract void Interact();

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("On Highlight");
    }

    private void OnTriggerExit(Collider other) 
    {
        Debug.Log("Off Highlight");
    }
}
