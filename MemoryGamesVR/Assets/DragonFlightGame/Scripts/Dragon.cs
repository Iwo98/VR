using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 startPosition;
    public float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPosition.z));
        //transform.position = Vector3.Lerp(transform.position, mousePosition, moveSpeed);
        transform.position = mousePosition;
    }
}
