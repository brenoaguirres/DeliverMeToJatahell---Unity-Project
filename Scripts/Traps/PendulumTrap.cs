using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumTrap : MonoBehaviour
{
    [SerializeField]
    private AudioClip ropeSFX;
    [SerializeField]
    private AudioClip swingSFX;

    public void PlayRopeSFX()
    {
        AudioController.instance.PlayOneShot(ropeSFX);
    }

    public void PlaySwingSFX()
    {
        AudioController.instance.PlayOneShot(swingSFX);
    }
}
