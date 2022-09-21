using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public float speed = 10.0f; 
    public float xPos, yPos, zPos;
    public Vector3 desiredPos;
    public float timer = 0;
    public float timerSpeed = 0.2f;
    public float timeToMove = 1.0f;
    public int direction;
    public GameObject[] cubeGlob;
    private float posChangeProb = 0.3f;
    public static int maxPoints = 0;
    private int randCube;

    // Start is called before the first frame update
    void Start()
    {
        xPos = Random.Range(0, 1f);
        yPos = Random.Range(0, 1f);
        zPos = 7f;
        direction = 0;
        desiredPos = new Vector3(xPos, yPos, zPos);
        switch (GameManager.game_mode) {
            case 6:
                timerSpeed = 0.3f;
                break;
            case 7:
                timerSpeed = 0.4f;
                break;
            case 8:
                timerSpeed = 0.5f;
                posChangeProb = 0.5f;
                speed = 15;
                break;
            case 9:
                timerSpeed = 0.6f;
                posChangeProb = 0.5f;
                speed = 15;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * timerSpeed;
        if (timer >= timeToMove && GameManager.game_state == 1)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, desiredPos) <= 0.01f)
            {
                int oldDir = direction;
                if (GameManager.game_mode >= 3 && Random.Range(0, 1f) < posChangeProb)
                {
                    if (Random.Range(0, 1f) < 0.5f)
                    {
                        direction = (direction + 1) % 4;
                    }
                    else
                    {
                        direction = (direction + 3) % 4;
                    }
                }

                xPos = Random.Range(0, 1f);
                yPos = Random.Range(0, 1f);
                zPos = Random.Range(0, 1f);
                if (direction == 0)         zPos = 8;
                else if (direction == 1)    xPos = 8;
                else if (direction == 2)    zPos = -8;
                else if (direction == 3)    xPos = -8;
                desiredPos = new Vector3(xPos, yPos, zPos);
                timer = 0.0f;
                int range = 4;
                if (GameManager.game_mode == 0 || GameManager.game_mode == 3)
                    range = 1;
                if (GameManager.game_mode == 1 || GameManager.game_mode == 4)
                    range = 3;
                if (GameManager.game_mode == 2 || GameManager.game_mode == 5)
                    range = 4;
                randCube = Random.Range(0, range);
                GameObject cube = Instantiate(cubeGlob[randCube], new Vector3(transform.position.x, transform.position.y + 1.399f, transform.position.z), transform.rotation);
                cube.GetComponent<Cube>().setDirection(oldDir);
                if (randCube < 3) maxPoints++;
                //cube.transform.localPosition = Vector3.zero;

            }
        }

    }
}
