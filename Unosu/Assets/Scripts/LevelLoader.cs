//------------------------------------------------------------------------------
//
// File Name:	LevelLoader.cs
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

public class LevelLoader : MonoBehaviour
{
    public int CurrentLevel = 1;
    public GameObject[] levels;

    public void ResetLevel(int level)
    {
        //account for arrays starting at zero
        level = level - 1;
        if (FindObjectOfType<Level>() != null)
            Destroy(FindObjectOfType<Level>().gameObject);
        if (FindObjectOfType<PlayerController>() != null)
            Destroy(FindObjectOfType<PlayerController>().gameObject);
        Instantiate(levels[level]);
        FindObjectOfType<KeyDisplay>(true).ResetPlayer();
    }
    public void NextLevel()
    {
        CurrentLevel += 1;
        ResetLevel(CurrentLevel);
    }

    public void SetLevel(int level)
    {
        CurrentLevel = level;
        ResetLevel(CurrentLevel);
    }
}

