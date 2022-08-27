using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHands : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public Transform RPoint;
    public Transform LPoint;
    public float beat = 60 / 130;
    private float timer;
   // private bool isUp = false;

    //public Main myMain;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Spawn()
    {
        if (timer > beat)
        {
            GameObject lHand;
            GameObject rHand;

            rHand = Instantiate(rightHand, RPoint);
            lHand = Instantiate(leftHand, LPoint);
            rHand.transform.localPosition = Vector3.zero;
            //rHand.transform.Rotate(transform.forward);
            lHand.transform.localPosition = Vector3.zero;
            //lHand.transform.Rotate(transform.forward);
            timer -= beat;

          

            //myMain.spawnNumber += 1;
            //myMain.allText.text = (myMain.spawnNumber).ToString();
        }

        timer += Time.deltaTime;
    }
}
