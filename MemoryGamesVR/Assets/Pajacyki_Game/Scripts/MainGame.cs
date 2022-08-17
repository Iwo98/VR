using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameObject Plate;
    public GameObject Desk;
    public GameObject Instrument;
    private GameObject SpawnedPlate;

    public Transform SpawnPointLeft;
    public Transform SpawnPointRight;
    public float plateSpeed = 2f;
    public float score = 0;
    public bool isLeft = false;

    private PlateCollide plateCollide;
    private InstrumentsCollide instrumentsCollide;

    // Start is called before the first frame update
    void Awake()
    {
        plateCollide = Plate.GetComponent<PlateCollide>();
        instrumentsCollide = Instrument.GetComponent<InstrumentsCollide>();

        plateCollide.speed = plateSpeed;

        SpawnedPlate = Instantiate(Plate, SpawnPointLeft);
        SpawnedPlate.transform.localPosition = Vector3.zero;
        SpawnedPlate.transform.Rotate(-transform.forward);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(score);
        if (SpawnedPlate.gameObject && !SpawnedPlate.gameObject.active)
        {
            if (isLeft)
            {
                SpawnedPlate = Instantiate(Plate, SpawnPointLeft);
            }
            else
            {
                SpawnedPlate = Instantiate(Plate, SpawnPointRight);
            }
            SpawnedPlate.transform.localPosition = Vector3.zero;
            SpawnedPlate.transform.Rotate(-transform.forward);
            isLeft = !isLeft;
        }
    }
}
