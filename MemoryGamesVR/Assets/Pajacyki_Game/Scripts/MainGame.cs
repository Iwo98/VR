using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainGame : MonoBehaviour
{
    public GameObject Plate;
    public GameObject Desk;
    public GameObject Instrument;
    public TextMeshProUGUI allPoints;
    public Canvas StartingCanvas;

    public Transform SpawnPointLeft;
    public Transform SpawnPointRight;
    public float plateSpeed = 2f;
    public int score = 0;
    public int numberOfSpawnedPlates = 0;
    public bool isLeft = false;
    public bool isGameOn = false;
    public bool isFirstTime = true;

    private GameObject SpawnedPlate;
    private PlateCollide plateCollide;
    private InstrumentsCollide instrumentsCollide;

    // Start is called before the first frame update
    void Awake()
    {
        plateCollide = Plate.GetComponent<PlateCollide>();
        instrumentsCollide = Instrument.GetComponent<InstrumentsCollide>();
        plateCollide.speed = plateSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstTime && isGameOn)
        {
            SpawnedPlate = Instantiate(Plate, SpawnPointLeft);
            SpawnedPlate.transform.localPosition = Vector3.zero;
            SpawnedPlate.transform.Rotate(-transform.forward);
            isFirstTime = false;
        }

        if (isGameOn)
        {
            if (SpawnedPlate.gameObject != null && !SpawnedPlate.gameObject.activeSelf)
            {
                numberOfSpawnedPlates++;
                allPoints.text = numberOfSpawnedPlates.ToString();
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

    public void CLickStartButton()
    {
        isGameOn = true;
        StartingCanvas.gameObject.SetActive(false);
    }
}
