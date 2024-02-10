using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerZombie : MonoBehaviour
{
    #region Fields and Properties

    [SerializeField]
    private List<AudioClip> hurtSFX = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> deathSFX = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> attackSFX = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> moveSFX = new List<AudioClip>();

    #endregion
}
