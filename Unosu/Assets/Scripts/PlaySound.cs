//------------------------------------------------------------------------------
//
// File Name:	PlaySound.cs
// Author(s):	Tyler Dean (tyler.dean)
// Project:	GAM 6.0.0 ASSIGNMENT - Prototype 2
// Course:	WANIC VGP2
//
// Copyright © 2019 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------
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
