using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshKeys : MonoBehaviour
{
    public int KeyToRefresh = 0;
    // 1 = jump
    // 2 = left
    // 3 = right
    // 4 = slide?
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
            switch(KeyToRefresh)
            {
                case 1:
                    //write refresh code here
                    break;
            }
        }
    }
}
