using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiesEatingSoundMixer : MonoBehaviour
{
    private AudioSource[] audioSource;

    public AudioClip RippingFlesh;
    public AudioClip EatingGuts;
    public AudioClip ZombieEating1;
    public AudioClip ZombieEating2;

    public float timeToAudioMax = 9;
    public float timeToAudioMin = 5;
    private float timeToPlayAudio;

    private void Start()
    {
        timeToPlayAudio = Random.Range(timeToAudioMin, timeToAudioMax);
        audioSource = GetComponents<AudioSource>();
    }
    void Update()
    { 
        timeToPlayAudio -= Time.deltaTime;

        if (timeToPlayAudio <= 0)
        {
            timeToPlayAudio = Random.Range(timeToAudioMin, timeToAudioMax);
            float clipToPlay1 = Random.Range(0, 2);
            float clipToPlay2 = Random.Range(0, 2);

            switch (clipToPlay1)
            {
                case 0:
                    audioSource[0].PlayOneShot(RippingFlesh);
                    break;
                case 1:
                    audioSource[0].PlayOneShot(EatingGuts);
                    break;
            }

            switch (clipToPlay2)
            {
                case 0:
                    audioSource[1].PlayOneShot(ZombieEating1);
                    break;
                case 1:
                    audioSource[1].PlayOneShot(ZombieEating2);
                    break;
            }
        }
    }
}
