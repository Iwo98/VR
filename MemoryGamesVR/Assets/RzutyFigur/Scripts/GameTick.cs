using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTick : MonoBehaviour
{
    public float rotationParam = 5;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation *= Quaternion.Euler(0, rotationParam, 0);
    }
}
