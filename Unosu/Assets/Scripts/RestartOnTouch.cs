//------------------------------------------------------------------------------
//
// File Name:	RestartOnTouch.cs
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

public class RestartOnTouch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().PlayDeathSound();
            LevelLoader loader = FindObjectOfType<LevelLoader>();
            loader.ResetLevel(loader.CurrentLevel);
        }
    }
}
