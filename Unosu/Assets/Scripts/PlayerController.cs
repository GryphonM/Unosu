using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float slideSpeed;
    [SerializeField] Vector2 slideSize;
    [SerializeField] float slideLength;

    bool grounded;
    bool sliding;

    Rigidbody2D myRB;
    bool facingRight = true;
    Vector2 ogSize;

    bool canMoveRight = true;
    bool canMoveLeft = true;
    bool canJump = true;
    bool canSlide = true;
    
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        ogSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Paused)
        {
            // Move Right
            if (canMoveRight && Input.GetKey(GameManager.Controls.MoveRight))
            {
                Vector2 newVel = new Vector2(walkSpeed, myRB.velocity.y);
                myRB.velocity = newVel;

                if (!facingRight)
                    facingRight = !facingRight;
            }

            // Move Left
            if (canMoveLeft && Input.GetKey(GameManager.Controls.MoveLeft))
            {
                Vector2 newVel = new Vector2(-walkSpeed, myRB.velocity.y);
                myRB.velocity = newVel;

                if (facingRight)
                    facingRight = !facingRight;
            }

            // Jump
            if (canJump && grounded && Input.GetKeyDown(GameManager.Controls.Jump))
            {
                Vector2 newVel = new Vector2(myRB.velocity.x, jumpSpeed);
                myRB.velocity = newVel;
            }

            // Slide
            if (canSlide && Input.GetKeyDown(GameManager.Controls.Slide))
            {
                Vector2 newVel = new Vector2(slideSpeed, myRB.velocity.y);
                myRB.velocity = newVel;
            }
        }
    }
}
