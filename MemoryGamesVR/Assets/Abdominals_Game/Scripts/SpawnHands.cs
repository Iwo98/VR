using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnHands : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;
    public Transform RPoint;
    public Transform LPoint;
    public float beat = 10/10;
    private float timer;
   // private bool isUp = false;

    public MainAbdominals myMain;

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
            lHand.transform.localPosition = Vector3.zero;
            timer -= beat;
            myMain.spawnNumber += 2;
            myMain.allText.text = (myMain.spawnNumber).ToString();


          
        }

        timer += Time.deltaTime;
    }
}
