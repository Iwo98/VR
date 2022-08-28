          using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainAbdominals : MonoBehaviour
{
    public GameObject Spawner;
    public GameObject HandR;
    public GameObject HandL;

    public Canvas StartMenuCanvas;
    public Canvas EndMenuCanvas;
    public int score = 0;
    public int phase = 0;
    public float maxTime;
    public int spawnNumber = 0;
    public TextMeshProUGUI allText;
    public TextMeshProUGUI gainedText;
    public TextMeshProUGUI finalText;

    private SpawnHands spawnHands;
    private HandCollider handColliderR;
    private HandCollider handColliderL;
    private float currTime = 0;



    // Start is called before the first frame update
    void Start()
    {   
        spawnHands = Spawner.GetComponent<SpawnHands>();
        handColliderR = new HandCollider();
        handColliderL = HandL.GetComponent<HandCollider>();
        handColliderL.ResetPoints();
    }

    // Update is called once per frame
    void Update()
    {
         if (phase == 1) 
        { 
            spawnHands.Spawn();
            currTime += Time.deltaTime;

            if (currTime > maxTime)
            {
                phase = 2;
                currTime = 0;
            }
        }
        else if (phase == 2)
        {
            currTime += Time.deltaTime;
            if (currTime > 15)
            {                
                phase = 3;
            }

        }else if (phase == 3)
        {   
            EndMenuCanvas.gameObject.SetActive(true);
            finalText.text = (Math.Round((score * 100.0 / spawnNumber))).ToString() + "%";
            phase = 4;
        }
    }
    
}
