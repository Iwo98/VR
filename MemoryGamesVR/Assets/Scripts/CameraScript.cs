using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{

    public float speedH = 3.0f;
    public float speedV = 3.0f;

    private float yaw = 0;
    private float pitch = 0;

    Camera m_MainCamera;

    // Use this for initialization
    void Start()
    {
        m_MainCamera = Camera.main;
        //Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) {

            yaw = speedH * Input.GetAxis("Mouse X");
            pitch = speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = transform.eulerAngles + new Vector3(-pitch, yaw, 0.0f);
        }

        if (Input.GetMouseButtonDown(1))
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetMouseButtonUp(1))
            Cursor.lockState = CursorLockMode.Confined;

        //restart scene with ctrl + r
        if (Input.GetKeyDown("r") && Input.GetKey(KeyCode.LeftControl))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //restart scene with ctrl + r
        if (Input.GetKeyDown("escape"))
            Application.Quit();


    }
}
