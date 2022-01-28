using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePads : MonoBehaviour
{
    public float thrust = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
