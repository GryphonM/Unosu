using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    public void NextLevel()
    {
        FindObjectOfType<KeyDisplay>(true).gameObject.SetActive(true);
        FindObjectOfType<LevelLoader>().NextLevel();
    }
}
