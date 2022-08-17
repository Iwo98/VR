using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlates : MonoBehaviour
{
    public GameObject Plate;
    public Transform SpawnPointLeft;
    public Transform SpawnPointRight;
    public bool isLeft = false;
    public bool isSpawned = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(isSpawned == false)
        {
            isSpawned = true;
            GameObject plate;
            if (isLeft)
            {
                plate = Instantiate(Plate, SpawnPointLeft);
            }
            else
            {
                plate = Instantiate(Plate, SpawnPointRight);
            }
            plate.transform.localPosition = Vector3.zero;
            plate.transform.Rotate(-transform.forward);
            isLeft = !isLeft;
        }
    }
}
