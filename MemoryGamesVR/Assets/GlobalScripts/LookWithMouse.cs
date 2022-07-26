using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWithMouse : MonoBehaviour
{
    public GameObject xr_camera;

    public float speedH = 1.5f;
    public float speedV = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 rot = xr_camera.GetComponent<Transform>().eulerAngles;
            rot.x -= Input.GetAxis("Mouse Y") * speedV;
            rot.y += Input.GetAxis("Mouse X") * speedH;
            xr_camera.GetComponent<Transform>().eulerAngles = rot;
        }
    }
}
