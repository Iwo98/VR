using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timers : MonoBehaviour
{
    public float max_time;
    public float current_time;
    public bool counting_in_progress=true;

    public Material universalMaterial;
    public GameObject endMenu;
    public GameObject pointsCounter;

    public Slider slider;

    public float rememberTime;

    public List<GameObject> arrows;

    private bool changeTargets = true;

    // Start is called before the first frame update
    void Start()
    {
        counting_in_progress = false;
        
        max_time = 22f; // czas w s
        current_time = 0f;
        rememberTime = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_time < rememberTime && counting_in_progress)
        {
            current_time += Time.deltaTime;
            slider.value = current_time / rememberTime;
            foreach(var arrow in arrows)
            {
                arrow.SetActive(false);
            }
        }
        else if (current_time >= rememberTime && counting_in_progress && current_time <= rememberTime+max_time)
        {
            foreach(var arrow in arrows)
            {
                arrow.SetActive(true);
            }
            GameObject myObject = GameObject.Find("Better_Targets");
            if (changeTargets)
            {
                for (int i = 0; i < myObject.transform.childCount; i++)
                {
                    myObject.transform.GetChild(i).GetComponent<Renderer>().material = universalMaterial;
                }
                changeTargets = false;
            }
            current_time += Time.deltaTime;
            slider.value = (current_time - rememberTime) / max_time;
        }
        else if (current_time > 0)
        {
            pointsCounter.GetComponent<TMPro.TextMeshProUGUI>().text = pointsCounter.GetComponent<PointsCounter>().GetPoints();
            slider.value = 0;
            counting_in_progress = false;
            current_time = 0f;
            changeTargets = true;
            endMenu.SetActive(true);
        }
    }


    public void ResetLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void SetTimer(long max_t)
    {
        max_time = max_t;
    }

    public void startTimeCounter()
    {
        counting_in_progress = true;
    }

}
