using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainGame : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Desk;
    public TextMeshProUGUI allPointsOnBook;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI timeText;
    public Canvas StartingCanvas;
    public Canvas EndCanvas;

    public Transform SpawnPointLeft;
    public Transform SpawnPointRight;
    public float ballSpeed = 2f;
    public float maxTime = 5f;
    public int phase = 1;
    public float score = 0;
    public float numberOfBounces = 0;
    public float currTime = 0;

    private GameObject SpawnedBall;
    private BallCollide ballCollide;
    private float result = 0;
    private bool shouldSpawnBall = true;


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
            timeText.text = Math.Round(currTime).ToString();

            if (shouldSpawnBall)
            {
                SpawnedBall = Instantiate(Ball, SpawnPointLeft);
                SpawnedBall.transform.localPosition = Vector3.zero;
                SpawnedBall.transform.Rotate(transform.forward);
                shouldSpawnBall = false;
            }

            if (currTime < maxTime)
            {
                allPointsOnBook.text = numberOfBounces.ToString();
            }
            else
            {
                Destroy(SpawnedBall);
                phase = 3;
                currTime = maxTime;
                result = (float)Math.Ceiling((score / numberOfBounces) * 100);
            }
        }
        else if (phase == 3)
        {
            resultText.text = result.ToString();
            EndCanvas.gameObject.SetActive(true);
        }
    }

    public void ClickStartButton()
    {
        phase = 2;
        StartingCanvas.gameObject.SetActive(false);
    }

    public void ClickEndButton()
    {
        Debug.Log("Koniec");
    }
}
