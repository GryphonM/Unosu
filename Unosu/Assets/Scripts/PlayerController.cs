using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    [Space(10)]

    [Tooltip("Initial speed added to the player when sliding")]
    [SerializeField] float slideSpeed;
    [Tooltip("Size player becomes what sliding\nMay be swapped out if we do animation")]
    [SerializeField] Vector2 slideSize;
    [Tooltip("The x velocity at which the player stops sliding")]
    [SerializeField] float slideStop;
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

    Rigidbody2D myRB;
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
        ogRot = transform.rotation;
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
                walking = true;
            }

            // Move Left
            if (canMoveLeft && !sliding && Input.GetKey(GameManager.Controls.MoveLeft))
            {
                Vector2 newVel = new Vector2(-walkSpeed, myRB.velocity.y);
                myRB.velocity = newVel;

                if (facingRight)
                    facingRight = !facingRight;
                walking = true;
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
                // Stop Sliding
                if (sliding)
                {
                    StopSliding();
                }

                Vector2 newVel = new Vector2(myRB.velocity.x, jumpSpeed);
                myRB.velocity = newVel;

                if (lockKeys)
                {
                    canJump = false;
                }
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

                transform.localScale *= slideSize;
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - (slideSize.y / 2));

                sliding = true;

                if (lockKeys)
                {
                    canSlide = false;
                }
            }
            if (sliding && !diving && ((facingRight && myRB.velocity.x <= slideStop) || (!facingRight && myRB.velocity.x >= -slideStop)))
            {
                StopSliding();
            }

            // Restart
            if (Input.GetKeyDown(GameManager.Controls.Reset))
            {
                canMoveLeft = true;
                canMoveRight = true;
                canJump = true;
                canSlide = true;
                LevelLoader loader = FindObjectOfType<LevelLoader>();
                loader.ResetLevel(loader.CurrentLevel);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (diving && collision.gameObject.CompareTag("Floor"))
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
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Floor"))
            grounded = false;
    }

    private void StopSliding()
    {
        transform.localScale /= slideSize;
        Vector2 newPos = transform.localPosition;
        if (facingRight)
            newPos.x += Mathf.Abs(transform.localScale.x - slideSize.x) / 2;
        else
            newPos.x -= Mathf.Abs(transform.localScale.x - slideSize.x) / 2;
        transform.localPosition = newPos;
        sliding = false;
    }
}
