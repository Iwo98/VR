using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public float getSoundIntensity(float distance)  // Sound intensity over distance
    {
        float intensity = 1.0f / (distance * distance);  // I = 1 / r^2
        return intensity;
    }
}
