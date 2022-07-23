using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockAsTimer : MonoBehaviour
{
    private float currentTime;
    private float maxTime;
    private GameObject pointer;

    void Start()
    {
        pointer = transform.Find("rotation_axis_pointer").gameObject;

        currentTime = 0.0f;
        maxTime = 1.0f;
    }

    void Update()
    {
        if (maxTime < 0)
        {
            float rotationPointer = 0.0f;
            pointer.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationPointer);
        }
        else
        {
            float rotationPointer = 360.0f * (currentTime / maxTime);
            pointer.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationPointer);
        }

    }

    public void setMaxTime(float time)
    {
        maxTime = time;
    }

    public void setCurrTime(float time)
    {
        currentTime = time;
    }
}
