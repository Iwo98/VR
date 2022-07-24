using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithKeyboard : MonoBehaviour
{
    public GameObject xr_camera;

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = xr_camera.GetComponent<Transform>().localPosition;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            mov.y -= Input.GetAxis("Jump") * Time.deltaTime * 10;
        }
        else
        {
            mov.y += Input.GetAxis("Jump") * Time.deltaTime * 10;
        }


        mov.x += Input.GetAxis("Horizontal") * Time.deltaTime * 20;
        mov.z += Input.GetAxis("Vertical") * Time.deltaTime * 20;
        xr_camera.GetComponent<Transform>().localPosition = mov;
    }
}

