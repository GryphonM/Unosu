using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float duration = 1;
    public float detectionDist = .1f;
    public GameObject Point1;
    public GameObject Point2;
    float timer = 0;
    bool MoveToOne = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (MoveToOne == true)
        {
            if(detectionDist > Vector3.Distance(Point1.gameObject.transform.position, this.gameObject.transform.position))
            {
                MoveToOne = false;
                timer = 0;
            }
            this.transform.position = Vector3.Lerp(this.gameObject.transform.position, Point1.gameObject.transform.position, timer / duration);
            timer += Time.deltaTime;
        }
        else
        {
            if (detectionDist > Vector3.Distance(Point2.gameObject.transform.position, this.gameObject.transform.position))
            {
                MoveToOne = true;
                timer = 0;
            }
            this.transform.position = Vector3.Lerp(this.gameObject.transform.position, Point2.gameObject.transform.position, timer / duration);
            timer += Time.deltaTime;
        }
    }
}
