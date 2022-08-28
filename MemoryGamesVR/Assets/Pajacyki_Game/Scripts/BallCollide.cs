using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollide : MonoBehaviour
{
    public float speed = 1f;
    public float isLeft = 1f;
    public GameObject gameController;

    private MainGame mainGame;

    private void Start()
    {
        gameController = GameObject.Find("GameController");
        mainGame = gameController.GetComponent<MainGame>();
    }
    public void Update()
    {
        transform.position += transform.forward * isLeft * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "BucketEnd")
        {
            mainGame.numberOfBounces++;
            isLeft *= -1f;
        }
    }
}
