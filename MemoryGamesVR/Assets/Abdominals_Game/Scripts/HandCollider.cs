using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    public AudioSource ScoreSound;
    public static int points = 0;
    public int allPoints = 0;
    public MainAbdominals myMain;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "HandPoint")
        {
            points += 1;
            //Debug.Log(points);
            myMain.gainedText.text = (points).ToString();
            myMain.score = points;
            ScoreSound.Play();
        }

        if (collider.gameObject.tag == "Hand" && this.tag == "HandPoint" )
        {
            Destroy(this.gameObject, 1.0f);
        }
            
    }
    public void ResetPoints()
    {
        points = 0;
    }
}
