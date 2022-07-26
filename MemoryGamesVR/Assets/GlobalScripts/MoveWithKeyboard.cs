using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithKeyboard : MonoBehaviour
{
    public GameObject xr_camera;
    public float moveSpeed = 25;

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        xr_camera.transform.Translate(0f, 0f, move);
        xr_camera.GetComponent<Transform>().Translate(0f, 0f, move);
    }
}

