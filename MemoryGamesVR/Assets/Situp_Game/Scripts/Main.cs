using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject Spawner;
    public GameObject Head;

    public Canvas StartMenuCanvas;
    //public Canvas EndMenuCanvas;
    public int score = 0;
    public int phase = 0;
    public float maxTime;
    public int spawnNumber = 0;

    private SpawnItems spawnItems;
    private CubeCollider cubeCollider;
    private float currTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        spawnItems = Spawner.GetComponent<SpawnItems>();
        cubeCollider = Head.GetComponent<CubeCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 1)
        {
            spawnItems.Spawn();
            currTime += Time.deltaTime;
            if (currTime > maxTime)
            {
                phase = 2;
                currTime = 0;
            }
        }
        else if (phase == 2)
        {
            currTime += Time.deltaTime;
            if (currTime > 7)
            {                
                score = cubeCollider.points;
                Debug.Log(score);
                Debug.Log(spawnNumber);
                phase = 3;
            }

        }
    }

}
