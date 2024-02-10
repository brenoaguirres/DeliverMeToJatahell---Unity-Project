using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject aimCamera;
    [SerializeField] private GameObject aimReticle;
    [SerializeField] private CharacterAnimator anim;

    private void Start() 
    {
        anim = GameObject.FindWithTag("Player").GetComponent<CharacterAnimator>();
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
        }
    }

    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(.25f);
        aimReticle.SetActive(enabled);
    }

}
