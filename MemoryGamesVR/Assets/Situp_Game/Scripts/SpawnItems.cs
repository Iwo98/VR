using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject GoldCube;
    public Transform UpPoint;
    public Transform DownPoint;
    public float beat = 60/130;
    private float timer;
    private bool isUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Spawn()
    {
        if(timer > beat)
        {
            GameObject cube;
            if (isUp)
            {
                cube = Instantiate(GoldCube, UpPoint);
            }
            else
            {
                cube = Instantiate(GoldCube, DownPoint);
            }
            cube.transform.localPosition = Vector3.zero;
            cube.transform.Rotate(transform.forward);
            timer -= beat;
            isUp = !isUp;
        }

        timer += Time.deltaTime;
    }
}
