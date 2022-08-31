using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSign : MonoBehaviour
{
    private TMPro.TextMeshPro timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = transform.Find("Time Text").GetComponent<TMPro.TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime(string time)
    {
        timeText.text = time;
    }
}
