using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFrontBack : MonoBehaviour
{

    public GameObject objectToShowOrHide;
    public float objectDesiredHeight;
    public float desiredZLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (objectToShowOrHide.transform.position.y > objectDesiredHeight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -desiredZLevel);
        }
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, desiredZLevel);
    }
}
