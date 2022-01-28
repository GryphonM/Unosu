//------------------------------------------------------------------------------
//
// File Name:	BouncePads.cs
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

public class BouncePads : MonoBehaviour
{
    public float thrust = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 newVel = rb.velocity;
            newVel.y = thrust;
            rb.velocity = newVel;
            PlayerController pc = collision.GetComponent<PlayerController>();
            pc.PlayBounceSound();
        }
    }
}
