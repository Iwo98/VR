using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public AudioSource CeilingDown, CeilingUp;
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
        CeilingDown.Play();

        if (transform.position.y > 4)
        {
            transform.position -= transform.up * Time.deltaTime;
        }

    }

    public void GoUp()
    {
        CeilingUp.Play();

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

