//------------------------------------------------------------------------------
//
// File Name:	LevelTransition.cs
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

public class LevelTransition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelLoader lv = FindObjectOfType<LevelLoader>();
            lv.NextLevel();
        }
    }
}
