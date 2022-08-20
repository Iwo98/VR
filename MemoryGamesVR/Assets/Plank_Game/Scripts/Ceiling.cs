using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public bool startMove;
    // Start is called before the first frame update
    void Start()
    {
        startMove = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void GoDown()
    {
        if(transform.position.y > 4)
        {
            transform.position -= transform.up * Time.deltaTime;
        }

    }

    public void GoUp()
    {
        if (transform.position.y < 4.776)
        {
            transform.position += transform.up * Time.deltaTime;
        }

    }

    public void StopCeiling()
    {
        this.startMove = false;
    }
}

