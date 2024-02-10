using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRaycast : MonoBehaviour
{
    #region Fields and Properties
    // Raycast variables
    [SerializeField]
    private int rayLenght = 10;
    [SerializeField]
    private LayerMask layerMaskInteract;
    [SerializeField]
    private string excludeLayerName = null;

    private InteractableRefactored raycastedObj = null;

    // References player's interaction with door
    private bool hasInteracted = false;
    private bool doOnce = false;

    private const string interactableTag = "InteractiveObject";

    private Camera cam;
    #endregion

    #region Execution

    private void Start() 
    {
        cam = Camera.main;
    }

    private void Update() 
    {
        RaycastHit hit;

        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.green);
        
        //int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, rayLenght, layerMaskInteract))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                Debug.Log("Found interactable tag");
                if(!doOnce && hasInteracted)
                {
                    Debug.Log("!doOnce & has interacted");
                    raycastedObj = hit.collider.gameObject.GetComponent<InteractableRefactored>();
                    doOnce = true;
                }

                if (raycastedObj != null)
                {
                    Debug.Log("raycastedobj was not null");
                    // Interacts with object
                    raycastedObj.hasInteracted = true;
                    raycastedObj.Interact();
                    // Clean interaction parameters
                    hasInteracted = false;
                    doOnce = false;
                    raycastedObj = null;
                }

            }
            else
            {
                if (hasInteracted == true || raycastedObj != null)
                {
                    //Clean interaction parameters
                    hasInteracted = false;
                    doOnce = false;
                    raycastedObj = null;
                }
            }
        }
    }
    #endregion

    #region Methods
    
    public void OnInteractionButton()
    {
        hasInteracted = true;
    }

    #endregion
}
