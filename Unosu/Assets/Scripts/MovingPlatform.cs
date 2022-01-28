//------------------------------------------------------------------------------
//
// File Name:	MovignPlatform.cs
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

public class MovingPlatform : MonoBehaviour
{
    public float duration = 1;
    public float detectionDist = .1f;
    public GameObject Point1;
    public GameObject Point2;
    float timer = 0;
    bool MoveToOne = true;

    void FixedUpdate()
    {
        if (MoveToOne == true)
        {
            if (detectionDist > Vector3.Distance(Point1.gameObject.transform.position, this.gameObject.transform.position))
            {
                MoveToOne = false;
                timer = 0;
            }
            this.transform.position = Vector3.Lerp(this.gameObject.transform.position, Point1.gameObject.transform.position, timer / duration);
            timer += Time.fixedDeltaTime;
        }
        else
        {
            if (detectionDist > Vector3.Distance(Point2.gameObject.transform.position, this.gameObject.transform.position))
            {
                MoveToOne = true;
                timer = 0;
            }
            this.transform.position = Vector3.Lerp(this.gameObject.transform.position, Point2.gameObject.transform.position, timer / duration);
            timer += Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.transform.parent = this.gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
