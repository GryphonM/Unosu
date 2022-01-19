using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float endSlideSpeed;
    [SerializeField] float endSlideStop;
    bool endSliding = false;

    [SerializeField] float jumpSpeed;

    [SerializeField] float slideSpeed;
    [SerializeField] Vector2 slideSize;
    [SerializeField] float slideStop;

    bool grounded = false;
    bool sliding = false;

    Rigidbody2D myRB;
    bool facingRight = true;
    Vector2 ogSize;

    [HideInInspector] public bool canMoveRight = true;
    [HideInInspector] public bool canMoveLeft = true;
    [HideInInspector] public bool canJump = true;
    [HideInInspector] public bool canSlide = true;
    [SerializeField] bool lockKeys = true;
    
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
            if (canMoveRight && !sliding && Input.GetKey(GameManager.Controls.MoveRight))
            {
                Vector2 newVel = new Vector2(walkSpeed, myRB.velocity.y);
                myRB.velocity = newVel;

                if (!facingRight)
                    facingRight = !facingRight;
            }

            // Move Left
            if (canMoveLeft && !sliding && Input.GetKey(GameManager.Controls.MoveLeft))
            {
                Vector2 newVel = new Vector2(-walkSpeed, myRB.velocity.y);
                myRB.velocity = newVel;

                if (facingRight)
                    facingRight = !facingRight;
            }

            // Stop Moving
            if ((Input.GetKeyUp(GameManager.Controls.MoveRight) && canMoveRight) ||
                (Input.GetKeyUp(GameManager.Controls.MoveLeft) && canMoveLeft))
            {
                if (!sliding)
                    endSliding = true;

                if (lockKeys)
                {
                    if (Input.GetKeyUp(GameManager.Controls.MoveRight))
                        canMoveRight = false;
                    if (Input.GetKeyUp(GameManager.Controls.MoveLeft))
                        canMoveLeft = false;
                }
            }
            if (endSliding)
            {
                Vector2 newVel = myRB.velocity;
                if (facingRight)
                    newVel.x -= Time.deltaTime * endSlideSpeed;
                else
                    newVel.x += Time.deltaTime * endSlideSpeed;
                myRB.velocity = newVel;
                if ((facingRight && myRB.velocity.x <= endSlideStop) || (!facingRight && myRB.velocity.x >= -endSlideStop))
                {
                    newVel.x = 0;
                    myRB.velocity = newVel;
                    endSliding = false;
                }
            }

            // Jump
            if (canJump && grounded && Input.GetKeyDown(GameManager.Controls.Jump))
            {
                Vector2 newVel = new Vector2(myRB.velocity.x, jumpSpeed);
                myRB.velocity = newVel;
            }
            if (lockKeys)
            {
                if (Input.GetKeyUp(GameManager.Controls.Jump))
                    canJump = false;
            }

            // Slide
            if (canSlide && Input.GetKeyDown(GameManager.Controls.Slide))
            {
                Vector2 newVel = myRB.velocity;
                if (facingRight)
                    newVel.x = slideSpeed;
                else
                    newVel.x = -slideSpeed;
                myRB.velocity = newVel;

                transform.localScale = slideSize;
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - (slideSize.y / 2));

                sliding = true;
            }
            if (sliding && ((facingRight && myRB.velocity.x <= slideStop) || (!facingRight && myRB.velocity.x >= -slideStop)))
            {
                transform.localScale = ogSize;
                sliding = false;
            }
            if (lockKeys)
            {
                if (Input.GetKeyUp(GameManager.Controls.Slide))
                    canSlide = false;
            }

            // Restart
            if (Input.GetKeyDown(GameManager.Controls.Reset))
            {
                LevelLoader loader = FindObjectOfType<LevelLoader>();
                loader.ResetLevel(loader.CurrentLevel);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Floor"))
            grounded = false;
    }
}
