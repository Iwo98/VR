using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvas : MonoBehaviour
{
    private Vector3 firstPosition;
    private void Start()
    {
        firstPosition = transform.parent.position;
    }
    void Update()
    {
        Debug.Log(transform.parent.GetComponent<Timers>().current_time);
        if (transform.parent.GetComponent<Timers>().current_time == 0)
            transform.localScale = new Vector3(1, 1, 1);
    }


    public void setVoidInvisible()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
}
