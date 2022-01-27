using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
  
    public void PlayAudio()
    {
        audioSource.PlayOneShot(clip, volume);
    }
    
}
