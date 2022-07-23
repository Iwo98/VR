using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // simple rotation around the Y-axis
    public float speed;

    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
