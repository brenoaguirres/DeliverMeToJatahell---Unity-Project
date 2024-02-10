using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script creates a Singleton Pattern to manage all SFX from the game

public class AudioController : MonoBehaviour
{
    #region Fields and Properties
    // Singleton pattern
    private AudioSource _audioSource;
    public static AudioSource instance;
    #endregion

    #region Execution
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        instance = _audioSource;
    }
    #endregion

}
