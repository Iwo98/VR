using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Hand")
        {
           
            //ScoreSound.Play();
            //Destroy(this.gameObject, 1.0f);
            //AddPoint();

            Debug.Log("Buyyyyyya");
            //myMain.gainedText.text = (points).ToString();
            //Debug.Log(points);
        }

        if (collider.gameObject.tag == "HandPoint")
        {
            //ScoreSound.Play();
            Destroy(this.gameObject, 1.0f);
        }
            
    }
}
