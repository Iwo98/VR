using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class main : MonoBehaviour
{
    public GameObject Spikes;
    public GameObject Colider;
    public GameObject TextScore;
    //public TextMeshProUGUI TextScore;

    public Canvas StartMenuCanvas;
    public Canvas EndMenuCanvas;
    public float score = 0;
    public int finalScore = 0;
    public bool isGameOn = false;
    public float timeSpikes;
    public float maxTime;


    private Ceiling ceiling;
    private CollisionDetector collisionDetector;
    private TextMeshProUGUI textMeshProUGUI;

    private float currTime = 0;
    private bool timeOn = false;
    public int phase = 0;


    // Start is called before the first frame update
    void Start()
    {
        ceiling = Spikes.GetComponent<Ceiling>();
        collisionDetector = Colider.GetComponent<CollisionDetector>();
        textMeshProUGUI = TextScore.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isGameOn && ceiling.startMove)
        {
            timeOn = true;
            if (currTime > timeSpikes) ceiling.GoDown();
            if (currTime > timeSpikes + 1.5)
            {
                ceiling.startMove = false;
                phase = 1;
                currTime = 0;
            }
        }

        if (phase == 1)
        {
            if (collisionDetector.Points())
            {
                score += Time.deltaTime;
            }
            if (currTime > maxTime)
            {
                phase = 2;
                currTime = 0;
            }
        }
        else if (phase == 2)
        {
            ceiling.GoUp();
            finalScore = (int)Math.Round(score * 100 / maxTime);
            textMeshProUGUI.text = (Math.Round(score *100 / maxTime)).ToString() + "%";
            if (currTime > 2) phase = 3;
        }
        else if (phase == 3)
        {
            EndMenuCanvas.gameObject.SetActive(true);
        }

        if (timeOn)
        {
            currTime += Time.deltaTime;
        }

    }

    public void ClickStarButton()
    {
        isGameOn = true;
        StartMenuCanvas.gameObject.SetActive(false);
        ceiling.startMove = true;
    }




}
