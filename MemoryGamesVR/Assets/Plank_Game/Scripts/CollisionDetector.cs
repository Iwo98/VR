using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public bool tooHigh = false;
    public bool tooLow = false;
    public bool pointsOn = false;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "UpPlane")
        {
            tooHigh = true;
            pointsOn = false;
        }

        if (collider.gameObject.name == "DownPlane")
        {
            tooLow = true;
            pointsOn = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "UpPlane")
        {
            tooHigh = false;
            pointsOn = true;

        }

        if (collider.gameObject.name == "DownPlane")
        {
            tooLow = false;
            pointsOn = true;
        }
    }

    public bool Points()
    {
        if (pointsOn){
            return true;
        }
        else
        {
            return false;
        }
    }
}
