using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimingState : State
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject aimCamera;
    [SerializeField] private GameObject aimReticle;
    [SerializeField] private CharacterAnimator anim;
    // Checks if player is currently aiming for continuing this state or changing it
    private bool aiming = false;
    // Checks if player clicked aim button
    private bool callAim = false;

    // Variables to setup aim state
    private StateManager stateManager; 

    // Gets PlayerController to deactivate targetLock when deactivating aiming mode
    private PlayerController playerController;

    private void Start() 
    {
        anim = GameObject.FindWithTag("Player").GetComponent<CharacterAnimator>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        stateManager = GetComponentInParent<StateManager>();
    }

    public void SwitchCameras()
    {
        if (!aimCamera.activeInHierarchy)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
            anim.OutIdle();
            anim.Aim(true);
            StartCoroutine(ShowReticle());
        }
        else if (!mainCamera.activeInHierarchy)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimReticle.SetActive(false);
            anim.Aim(false);
            playerController.DeactivateTargetLock();
        }

        callAim = false;
    }

    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(.25f);
        aimReticle.SetActive(enabled);
    }
    public override State RunCurrentState()
    {
        if (callAim)
        {
            SwitchCameras();
        }
        
        if (aiming)
        {
            return this;
        }
        else
        {
            return null;
        }
    }

    public void SetAimState()
    {
        stateManager.SetState(this);
    }

    public void CallAim()
    {
        callAim = true;
    }
}
