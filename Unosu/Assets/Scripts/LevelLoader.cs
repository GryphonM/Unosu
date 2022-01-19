using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public int CurrentLevel = 1;
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
        //account for arrays starting at zero
        level = level - 1;        
        if (FindObjectOfType<Level>() != null)
            Destroy(FindObjectOfType<Level>().gameObject);
        Instantiate(levels[level]);
    }
    public void NextLevel()
    {
        CurrentLevel += 1;
        ResetLevel(CurrentLevel);
    }
}

