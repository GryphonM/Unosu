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
    Color DarkGrey = Color.black;
    Color UpColor;
    Color LeftColor;
    Color RightColor;
    Color SlideColor;

    // Start is called before the first frame update
    void Start()
    {
        playerCont = FindObjectOfType<PlayerController>();
        UpColor = Up.GetComponent<SpriteRenderer>().color;
        LeftColor = Left.GetComponent<SpriteRenderer>().color;
        RightColor = Right.GetComponent<SpriteRenderer>().color;
        SlideColor = Slide.GetComponent<SpriteRenderer>().color;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCont.canJump == true)
        {
            Up.GetComponent<SpriteRenderer>().material.color = UpColor;
        }
        else
            Up.GetComponent<SpriteRenderer>().material.color = DarkGrey;
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
}
