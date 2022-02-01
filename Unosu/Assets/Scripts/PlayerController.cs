//------------------------------------------------------------------------------
//
// File Name:	PlayerController.cs
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

public class PlayerController : MonoBehaviour
{
    public PlaySound jump;
    public PlaySound slide;
    public PlaySound collect;
    public PlaySound bounce;
    public PlaySound death;
    GameManager gm;
    [SerializeField] float walkSpeed;
    [Tooltip("Backwards force applied to the player when stopping \n" +
        "Higher = Faster Stop")]
    [SerializeField] float endSlideSpeed;
    [Tooltip("Speed at which player stops sliding and has their velocity set to 0 \n" +
        "Higher = Faster Stop \n" +
        "If this is above walk speed, player will stop instantly")]
    [SerializeField] float endSlideStop;
    bool endSliding = false;

    [Space(10)]

    [SerializeField] float jumpSpeed;
    [Tooltip("Distance from ground that player is considered grounded")]
    [SerializeField] float groundDist = 0.1f;
    [Tooltip("Distance from wall that player will stop colliding")]
    [SerializeField] float wallDist = 0.1f;

    [Space(10)]

    [Tooltip("Initial speed added to the player when sliding")]
    [SerializeField] float slideSpeed;
    [Tooltip("The x velocity at which the player stops sliding")]
    [SerializeField] float slideStop;
    [Tooltip("Time the player has to slide, if their velocity is 0 at the end, they will slide again")]
    [SerializeField] float slideTime = 0.3f;
    float slideTimer;
    [Tooltip("The velocity the player dives downwards at")]
    [SerializeField] Vector2 diveSpeed;
    [Tooltip("Rotation around the Z axis when diving")]
    [SerializeField] float diveRotation = 90;
    [Tooltip("Y change to position when finished diving")]
    [SerializeField] float positionOffset;

    bool grounded = false;
    bool sliding = false;
    bool diving = false;
    bool walking = false;
    bool hitWall = false;

    bool newLevel = true;

    Rigidbody2D myRB;
    SpriteRenderer mySR;
    BoxCollider2D myCol;
    Animator myAnim;
    Quaternion ogRot;

    [Space(10)]

    [SerializeField] bool facingRight = true;

    [HideInInspector] public bool canMoveRight = true;
    [HideInInspector] public bool canMoveLeft = true;
    [HideInInspector] public bool canJump = true;
    [HideInInspector] public bool canSlide = true;
    [SerializeField] bool lockKeys = true;
    
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        myCol = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();
        ogRot = transform.rotation;
        slideTimer = slideTime;
        gm = FindObjectOfType<GameManager>();
        mySR.flipX = !facingRight;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Paused)
        {
            if (Input.GetKeyDown(GameManager.Controls.MoveLeft) || Input.GetKeyDown(GameManager.Controls.MoveRight))
            {
                newLevel = false;
            }

            // Don't move if player was just spawned
            if (!newLevel)
            {
                // Move Right
                if (canMoveRight && !sliding && (!hitWall || grounded) && Input.GetKey(GameManager.Controls.MoveRight))
                {
                    Vector2 newVel = new Vector2(walkSpeed, myRB.velocity.y);
                    myRB.velocity = newVel;

                    if (!facingRight)
                    {
                        facingRight = !facingRight;
                        mySR.flipX = !facingRight;
                    }
                    walking = true;
                    myAnim.SetBool("Walking", true);
                }

                // Move Left
                if (canMoveLeft && !sliding && (!hitWall || grounded) && Input.GetKey(GameManager.Controls.MoveLeft))
                {
                    Vector2 newVel = new Vector2(-walkSpeed, myRB.velocity.y);
                    myRB.velocity = newVel;

                    if (facingRight)
                    {
                        facingRight = !facingRight;
                        mySR.flipX = !facingRight;
                    }
                    walking = true;
                    myAnim.SetBool("Walking", true);
                }

                // Stop Moving
                if ((Input.GetKeyUp(GameManager.Controls.MoveRight) && canMoveRight) ||
                    (Input.GetKeyUp(GameManager.Controls.MoveLeft) && canMoveLeft))
                {
                    if (!sliding)
                        endSliding = true;

                    if (lockKeys)
                    {
                        if (Input.GetKeyUp(GameManager.Controls.MoveRight) && walking)
                            canMoveRight = false;
                        if (Input.GetKeyUp(GameManager.Controls.MoveLeft) && walking)
                            canMoveLeft = false;
                    }

                    walking = false;
                    myAnim.SetBool("Walking", false);
                }
            }

            // Jump
            if (canJump && grounded && Input.GetKeyDown(GameManager.Controls.Jump))
            {
                // Stop Sliding
                if (sliding)
                {
                    StopSliding();
                }

                Vector2 newVel = new Vector2(myRB.velocity.x, jumpSpeed);
                myRB.velocity = newVel;
                jump.PlayAudio();
                myAnim.SetBool("Jumped", true);

                if (lockKeys)
                    canJump = false;
                if (newLevel)
                    newLevel = false;
            }

            // Slide
            if (canSlide && Input.GetKeyDown(GameManager.Controls.Slide))
            {
                endSliding = false;
                Vector2 newVel = myRB.velocity;
                if (grounded)
                {
                    if (facingRight)
                        newVel.x = slideSpeed;
                    else
                        newVel.x = -slideSpeed;
                }
                else
                {
                    if (facingRight)
                        newVel.x = diveSpeed.x;
                    else
                        newVel.x = -diveSpeed.x;
                    newVel.y = diveSpeed.y;
                    diving = true;

                    Vector3 newRot = transform.localRotation.eulerAngles;
                    if (facingRight)
                        newRot.z = -diveRotation;
                    else
                        newRot.z = diveRotation;
                    transform.rotation = Quaternion.Euler(newRot);
                }
                myRB.velocity = newVel;

                sliding = true;
                myAnim.SetBool("Sliding", true);
                slide.PlayAudio();

                if (lockKeys)
                {
                    canSlide = false;
                }
                if (newLevel)
                    newLevel = false;
            }
            // Prevent player from being stopped in their initial slide
            if (sliding)
            {
                if (slideTimer > 0)
                {
                    slideTimer -= Time.deltaTime;
                }
                
                if (slideTimer <= 0)
                {
                    if(((facingRight && myRB.velocity.x <= slideStop) || (!facingRight && myRB.velocity.x >= -slideStop)))
                    {
                        Vector2 newVel = myRB.velocity;
                        if (grounded)
                        {
                            if (facingRight)
                                newVel.x = slideSpeed;
                            else
                                newVel.x = -slideSpeed;
                        }
                        else
                        {
                            if (facingRight)
                                newVel.x = diveSpeed.x;
                            else
                                newVel.x = -diveSpeed.x;
                            newVel.y = diveSpeed.y;
                            diving = true;

                            Vector3 newRot = transform.localRotation.eulerAngles;
                            if (facingRight)
                                newRot.z = -diveRotation;
                            else
                                newRot.z = diveRotation;
                            transform.rotation = Quaternion.Euler(newRot);
                        }
                    }
                }
            }
            // Stop Sliding
            if (sliding && !diving && slideTimer <= 0 && ((facingRight && myRB.velocity.x <= slideStop) || (!facingRight && myRB.velocity.x >= -slideStop)))
            {
                StopSliding();
            }

            // Restart
            if (Input.GetKeyDown(GameManager.Controls.Reset))
            {
                LevelLoader loader = FindObjectOfType<LevelLoader>();
                loader.ResetLevel(loader.CurrentLevel);
            }

            // Floor and Wall Check
            {
                //Floor and Wall Check Variables
                float halfXScale = 0.5f * myCol.size.x;
                float halfYScale = 0.5f * myCol.size.y;

                // Floor Check
                RaycastHit2D midFloorHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + myCol.offset.y), Vector2.down, halfYScale + groundDist);
                RaycastHit2D rightFloorHit = Physics2D.Raycast(new Vector2(transform.position.x + halfXScale, transform.position.y + myCol.offset.y), Vector2.down, halfYScale + groundDist);
                RaycastHit2D leftFloorHit = Physics2D.Raycast(new Vector2(transform.position.x - halfXScale, transform.position.y + myCol.offset.y), Vector2.down, halfYScale + groundDist);
                if (midFloorHit.collider != null && midFloorHit.collider.CompareTag("Floor"))
                {
                    grounded = true;
                    myAnim.SetBool("Grounded", true);
                }
                else if (rightFloorHit.collider != null && rightFloorHit.collider.CompareTag("Floor"))
                {
                    grounded = true;
                    myAnim.SetBool("Grounded", true);
                }
                else if (leftFloorHit.collider != null && leftFloorHit.collider.CompareTag("Floor"))
                {
                    grounded = true;
                    myAnim.SetBool("Grounded", true);
                }
                else
                {
                    grounded = false;
                    myAnim.SetBool("Grounded", false);
                }

                // Wall Check
                RaycastHit2D midWallHit;
                RaycastHit2D topWallHit;
                RaycastHit2D bottomWallHit;
                if (facingRight)
                {
                    midWallHit = Physics2D.Raycast(transform.position, Vector2.right, halfYScale + wallDist);
                    topWallHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + halfYScale), Vector2.right, halfYScale + wallDist);
                    bottomWallHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfYScale), Vector2.right, halfYScale + wallDist);
                }
                else
                {
                    midWallHit = Physics2D.Raycast(transform.position, Vector2.left, halfYScale + wallDist);
                    topWallHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + halfYScale), Vector2.left, halfYScale + wallDist);
                    bottomWallHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfYScale), Vector2.left, halfYScale + wallDist);
                }

                if (midWallHit.collider != null && !midWallHit.collider.CompareTag("Harm"))
                    hitWall = true;
                else if (topWallHit.collider != null && !topWallHit.collider.CompareTag("Harm"))
                    hitWall = true;
                else if (bottomWallHit.collider != null && !bottomWallHit.collider.CompareTag("Harm"))
                    hitWall = true;
                else
                    hitWall = false;
            }
        }
    }

    // Make sure the end slide acts the same
    private void FixedUpdate()
    {
        if (endSliding)
        {
            Vector2 newVel = myRB.velocity;
            if (facingRight)
                newVel.x -= Time.fixedDeltaTime * endSlideSpeed;
            else
                newVel.x += Time.fixedDeltaTime * endSlideSpeed;
            myRB.velocity = newVel;
            if ((facingRight && myRB.velocity.x <= endSlideStop) || (!facingRight && myRB.velocity.x >= -endSlideStop))
            {
                newVel.x = 0;
                myRB.velocity = newVel;
                endSliding = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (diving)
            {
                Vector2 newVel = myRB.velocity;
                if (facingRight)
                    newVel.x = slideSpeed;
                else
                    newVel.x = -slideSpeed;
                myRB.velocity = newVel;
                transform.rotation = ogRot;

                Vector2 newPos = transform.position;
                newPos.y -= positionOffset;
                transform.position = newPos;

                diving = false;
            }

            myAnim.SetBool("Jumped", false);
        }
    }

    private void StopSliding()
    {
        sliding = false;
        slideTimer = slideTime;
        myAnim.SetBool("Sliding", false);
        myAnim.SetBool("Slid", false);
    }

    public void PlayCollectSound()
    {
        collect.PlayAudio();
    }

    public void PlayBounceSound()
    {
        bounce.PlayAudio();
    }

    public void PlayDeathSound()
    {
        death.PlayAudio();
    }
}
