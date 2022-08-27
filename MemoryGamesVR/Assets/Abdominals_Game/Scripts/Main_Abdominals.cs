using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Abdominals : MonoBehaviour
{
    public GameObject Spawner;

    private SpawnHands spawnHands;

    // Start is called before the first frame update
    void Start()
    {
        spawnHands = Spawner.GetComponent<SpawnHands>();
    }

    // Update is called once per frame
    void Update()
    {
         spawnHands.Spawn();
    }
}
