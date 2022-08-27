using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainAbdominals : MonoBehaviour
{
    public GameObject Spawner;
    public GameObject HandR;
    public GameObject HandL;

    public Canvas StartMenuCanvas;
    //public Canvas EndMenuCanvas;
    public int score = 0;
    public int phase = 0;
    public float maxTime;
    public int spawnNumber = 0;
    public TextMeshProUGUI allText;
    public TextMeshProUGUI gainedText;

    private SpawnHands spawnHands;
    private HandCollider handColliderR;
    private HandCollider handColliderL;
    private float currTime = 0;



    // Start is called before the first frame update
    void Start()
    { 
        spawnHands = Spawner.GetComponent<SpawnHands>();
        handColliderR = HandR.GetComponent<HandCollider>();
        handColliderL = HandL.GetComponent<HandCollider>();
    }

    // Update is called once per frame
    void Update()
    {
         if (phase == 1) 
        { 
            spawnHands.Spawn();
            currTime += Time.deltaTime;
            gainedText.text = (score).ToString();
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
                //score = handColliderR.points + handColliderL.points;
                //Debug.Log(score);
                Debug.Log(spawnNumber);
                phase = 3;
            }

        }
    }
    
}
