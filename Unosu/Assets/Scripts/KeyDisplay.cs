//------------------------------------------------------------------------------
//
// File Name:	KeyDisplay.cs
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

public class KeyDisplay : MonoBehaviour
{
    public GameObject Up;
    public GameObject Left;
    public GameObject Right;
    public GameObject Slide;
    PlayerController playerCont;
    public Color DarkGrey = Color.black;
    public Color UpColor;
    public Color LeftColor;
    public Color RightColor;
    public Color SlideColor;

    // Start is called before the first frame update
    void Start()
    {
        playerCont = FindObjectOfType<PlayerController>();
        Up.GetComponent<SpriteRenderer>().color = UpColor;
        Left.GetComponent<SpriteRenderer>().color = LeftColor;
        Right.GetComponent<SpriteRenderer>().color = RightColor;
        Slide.GetComponent<SpriteRenderer>().color = SlideColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCont.canJump == true)
        {
            Up.GetComponent<SpriteRenderer>().color = UpColor;
        }
        else
            Up.GetComponent<SpriteRenderer>().color = DarkGrey;
        if (playerCont.canMoveLeft == true)
        {
            Left.GetComponent<SpriteRenderer>().color = LeftColor;
        }
        else
            Left.GetComponent<SpriteRenderer>().color = DarkGrey;
        if (playerCont.canMoveRight == true)
        {
            Right.GetComponent<SpriteRenderer>().color = RightColor;
        }
        else
            Right.GetComponent<SpriteRenderer>().color = DarkGrey;
        if (playerCont.canSlide == true)
        {
            Slide.GetComponent<SpriteRenderer>().color = SlideColor;
        }
        else
            Slide.GetComponent<SpriteRenderer>().color = DarkGrey;
    }

    public void ResetPlayer()
    {
        playerCont = FindObjectOfType<PlayerController>();
    }
}
