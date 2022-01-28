//------------------------------------------------------------------------------
//
// File Name:	RefreshKeys.cs
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

public class RefreshKeys : MonoBehaviour
{
    [Tooltip("1 = Jump\n" +
            "2 = Left\n" +
            "3 = Right\n" +
            "4 = Slide")]
    public int KeyToRefresh = 0;
    // 1 = jump
    // 2 = left
    // 3 = right
    // 4 = slide
    PlayerController playerCont;
    SpriteRenderer mySR;

    [SerializeField] Color JumpColor;
    [SerializeField] Color LeftColor;
    [SerializeField] Color RightColor;
    [SerializeField] Color SlideColor;

    // Start is called before the first frame update
    void Start()
    {
        playerCont = FindObjectOfType<PlayerController>();
        mySR = GetComponent<SpriteRenderer>();
        switch(KeyToRefresh)
        {
            case 0:
                Debug.Log("Key to refresh not assigned");
                break;
            case 1:
                mySR.color = JumpColor;
                break;
            case 2:
                mySR.color = LeftColor;
                break;
            case 3:
                mySR.color = RightColor;
                break;
            case 4:
                mySR.color = SlideColor;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch(KeyToRefresh)
            {
                case 0:
                    Debug.Log("Key to refresh not asigned");
                    Destroy(this.gameObject);
                    break;
                case 1:
                    playerCont.PlayCollectSound();
                    playerCont.canJump = true;
                    Destroy(this.gameObject);
                    break;
                case 2:
                    playerCont.PlayCollectSound();
                    playerCont.canMoveLeft = true;
                    Destroy(this.gameObject);
                    break;
                case 3:
                    playerCont.PlayCollectSound();
                    playerCont.canMoveRight = true;
                    Destroy(this.gameObject);
                    break;
                case 4:
                    playerCont.PlayCollectSound();
                    playerCont.canSlide = true;
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
