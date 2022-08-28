using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
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
        }

        if (collider.gameObject.tag == "Hand" && this.tag == "HandPoint" )
        {
            //ScoreSound.Play();
            Destroy(this.gameObject, 1.0f);
        }
            
    }
    public void ResetPoints()
    {
        points = 0;
    }
}
