using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShowHints : MonoBehaviour
{
    public TextMeshProUGUI TooHighText;
    public TextMeshProUGUI TooLowText;
    public TextMeshProUGUI TimerText;
    public GameObject MainCamera;
    private CollisionDetector collisionDetector;
    public main myMain;

    private float TimeLeft = 0;
    // Start is called before the first frame update
    void Start()
    {
        collisionDetector = MainCamera.GetComponent<CollisionDetector>();
        TimeLeft = myMain.maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(collisionDetector.tooHigh && myMain.phase == 1)
        {
            TooHighText.text = "Jesteœ za wysoko";
        }
        else
        {
            TooHighText.text = "";
        }

        if (collisionDetector.tooLow && myMain.phase == 1)
        {
            TooLowText.text = "Jesteœ za nisko";
        }
        else
        {
            TooLowText.text = "";
        }

        if (myMain.phase == 1)
        {
            TimerText.text = (Math.Round(TimeLeft -= Time.deltaTime)).ToString();
        }
        else TimerText.text = "";
    }
}

