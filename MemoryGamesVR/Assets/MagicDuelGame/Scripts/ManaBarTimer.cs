using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBarTimer : MonoBehaviour
{
    private float currentTime;
    private float maxTime;
    private Vector3 startManaBarScale;

    public GameObject manaBar;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0.0f;
        maxTime = 1.0f;
        startManaBarScale = manaBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if ((maxTime - currentTime) < 0)
        {
            Vector3 scale = startManaBarScale;
            scale.x = 0.0f;
            manaBar.transform.localScale = scale;
        }
        else
        {
            Vector3 scale = startManaBarScale;
            scale.x *= (maxTime - currentTime) / maxTime;
            manaBar.transform.localScale = scale;
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
