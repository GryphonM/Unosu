//------------------------------------------------------------------------------
//
// File Name:	GameManager.cs
// Author(s):	Gryphon McLaughlin (gryphon.mclaughlin)
// Project:	GAM 6.0.0 ASSIGNMENT - Prototype 2
// Course:	WANIC VGP2
//
// Copyright © 2019 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public struct Keys
    {
        public Keys(KeyCode left,
            KeyCode right,
            KeyCode jump,
            KeyCode slide,
            KeyCode reset)
        {
            MoveLeft = left;
            MoveRight = right;
            Jump = jump;
            Slide = slide;
            Reset = reset;
        }

        public KeyCode MoveLeft;
        public KeyCode MoveRight;
        public KeyCode Jump;
        public KeyCode Slide;
        public KeyCode Reset;
    }

    public static Keys Controls = new Keys(KeyCode.A,
                                            KeyCode.D,
                                            KeyCode.Space,
                                            KeyCode.LeftShift,
                                            KeyCode.R);

    public static bool Paused = false;
}