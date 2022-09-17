using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeCollider : MonoBehaviour
{
    public AudioSource ScoreSound;
    public int points = 0;
    public Main myMain;

    void Start()
    {
        
    }
    
      
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Colide")
        {
           

            this.points += 1;
            myMain.gainedText.text = (points).ToString();
            //Debug.Log(points);
        }

        if (collider.gameObject.tag == "MainCamera")
        {
            ScoreSound.Play();
            Destroy(this.gameObject, 1.0f);
        }
            
    }

}
