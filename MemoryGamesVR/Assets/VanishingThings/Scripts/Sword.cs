using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private GameObject currentGameObject;
    public int cubeLayerId;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        currentGameObject = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        /*
        Debug.Log(collision.gameObject.layer);
        //GetComponent<AudioSource>().Play();
        if (collision.gameObject.layer == cubeLayerId)
        {
            ScoreScript.scoreUp();
        }
        else
        {
            ScoreScript.scoreDown();
        }
        Destroy(collision.gameObject);
        */
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.layer);
        gameObject.GetComponent<AudioSource>().PlayOneShot(clip); 
        if (collision.gameObject.layer == cubeLayerId || collision.gameObject.layer == 9)
        {
            ScoreScript.scoreUp();
        } 
        else
        {
            ScoreScript.scoreDown();
        }
        Destroy(collision.gameObject);
    }
}
