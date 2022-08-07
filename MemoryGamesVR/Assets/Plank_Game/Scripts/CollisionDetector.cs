using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public bool tooHigh = false;
    public bool tooLow = false;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "UpPlane")
        {
            tooHigh = true;
        }

        if (collider.gameObject.name == "DownPlane")
        {
            tooLow = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "UpPlane")
        {
            tooHigh = false;

        }

        if (collider.gameObject.name == "DownPlane")
        {
            tooLow = false;
        }
    }
}
