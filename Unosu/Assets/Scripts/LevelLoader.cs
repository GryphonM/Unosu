using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject[] levels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLevel(int level)
    {
        GameObject oldLevel = FindObjectOfType<Level>().gameObject;
        if (oldLevel != null)
            Destroy(oldLevel);
        Instantiate(levels[level]);
    }
}
