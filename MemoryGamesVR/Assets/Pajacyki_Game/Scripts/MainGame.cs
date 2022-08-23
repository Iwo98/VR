using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainGame : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Desk;
    public TextMeshProUGUI allPoints;
    public Canvas StartingCanvas;

    public Transform SpawnPointLeft;
    public Transform SpawnPointRight;
    public float ballSpeed = 2f;
    public float maxTime = 5f;
    public int phase = 0;
    public int score = 0;
    public int numberOfSpawnedPlates = 0;
    public bool isLeft = false;

    private GameObject SpawnedPlate;
    private BallCollide ballCollide;
    private float currTime = 0;
    private bool spawnFirstTime = true;


    // Start is called before the first frame update
    void Awake()
    {
        ballCollide = Ball.GetComponent<BallCollide>();
        ballCollide.speed = ballSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (phase == 1)
        {
            StartingCanvas.gameObject.SetActive(true);
        }
        else if (phase == 2)
        {
            currTime += Time.deltaTime;
            if (spawnFirstTime)
            {
                SpawnedPlate = Instantiate(Ball, SpawnPointLeft);
                SpawnedPlate.transform.localPosition = Vector3.zero;
                SpawnedPlate.transform.Rotate(-transform.forward);
                spawnFirstTime = false;
            }

            if (currTime < maxTime)
            {
                if (SpawnedPlate.gameObject != null && !SpawnedPlate.gameObject.activeSelf)
                {
                    numberOfSpawnedPlates++;
                    allPoints.text = numberOfSpawnedPlates.ToString();
                    if (isLeft)
                    {
                        SpawnedPlate = Instantiate(Ball, SpawnPointLeft);
                    }
                    else
                    {
                        SpawnedPlate = Instantiate(Ball, SpawnPointRight);
                    }
                    SpawnedPlate.transform.localPosition = Vector3.zero;
                    SpawnedPlate.transform.Rotate(-transform.forward);
                    isLeft = !isLeft;
                }
            }
            else
            {
                phase = 3;
                currTime = maxTime;
            }

        }
        else if (phase == 3)
        {
            Debug.Log("Koniec!");
        }
    }

    public void CLickStartButton()
    {
        phase = 2;
        StartingCanvas.gameObject.SetActive(false);
    }
}
