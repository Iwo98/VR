using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cubeGlob;
    public Transform[] points;
    public float beat = 60 / 105 * 3;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (timer > beat) {
        //    GameObject cube = Instantiate(cubeGlob, points[Random.Range(0, 3)]);
        //    cube.transform.localPosition = Vector3.zero;
        //    timer -= beat;
        //}
        //timer += Time.deltaTime;
    }
}
