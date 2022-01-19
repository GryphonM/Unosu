using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartOnTouch : MonoBehaviour
{
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
            LevelLoader loader = FindObjectOfType<LevelLoader>();
            loader.ResetLevel(loader.CurrentLevel);
        }
    }
}