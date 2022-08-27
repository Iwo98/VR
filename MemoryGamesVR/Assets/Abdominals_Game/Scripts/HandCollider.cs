using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    public int points = 0;
    public int allPoints = 0;
    public MainAbdominals myMain;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "HandPoint")
        {
           
            //ScoreSound.Play();
            //Destroy(this.gameObject, 1.0f);
            //AddPoint();

            Debug.Log("Buyyyyyya");
            myMain.score += 1;
            Debug.Log(points);
            //allPoints += this.points;
            //Debug.Log(points);
            //myMain.gainedText.text = (allPoints).ToString();
        }

        if (collider.gameObject.tag == "Hand" && this.tag == "HandPoint" )
        {
            //ScoreSound.Play();
            Destroy(this.gameObject, 1.0f);
        }
            
    }
}
