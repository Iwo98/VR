using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    Image timer;
    public float maxTime = 5f;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timer = this.GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        while (timeLeft > 0) 
        {
            timeLeft -= Time.deltaTime;
            timer.fillAmount = timeLeft / maxTime;
        }
    }
}
