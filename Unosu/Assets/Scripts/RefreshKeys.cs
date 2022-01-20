using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshKeys : MonoBehaviour
{
    public int KeyToRefresh = 0;
    // 1 = jump
    // 2 = left
    // 3 = right
    // 4 = slide
    PlayerController playerCont;
    // Start is called before the first frame update
    void Start()
    {
        playerCont = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    playerCont.canJump = true;
                    Destroy(this.gameObject);
                    break;
                case 2:
                    playerCont.canMoveLeft = true;
                    Destroy(this.gameObject);
                    break;
                case 3:
                    playerCont.canMoveRight = true;
                    Destroy(this.gameObject);
                    break;
                case 4:
                    playerCont.canSlide = true;
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
