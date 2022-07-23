using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterShoot : MonoBehaviour
{
    public int targetType; // (0 - cel uniwersalny, 1 - czewony, 2 - zielony, 3 - niebieski)
    
    public float timer;
    private float deltaTime;
    public GameObject pointsCounter;

    
    // Start is called before the first frame update
    void Start()
    {
        deltaTime = 0.1f;
        timer = 10f;
        setTargetType();
    }

    void setTargetType()
    {
        if (gameObject.transform.name.Contains("Red"))
            targetType = 1;
        else if (gameObject.transform.name.Contains("Green"))
            targetType = 2;
        else if (gameObject.transform.name.Contains("Blue"))
            targetType = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
       
        if (collision.collider.name.Contains("Arrow")) {
            switch (targetType)
            {
                case 1:
                    if (collision.collider.name.Contains("Red"))
                    {
                        pointsCounter.GetComponent<PointsCounter>().UpPoints(10);
                        Destroy(gameObject);
                    }
                    else
                    {
                        //environmentObject.GetComponent<PointsCounter>().DownPoints(2);
                    }
                    break;
                case 2:
                    if (collision.collider.name.Contains("Green"))
                    {
                        pointsCounter.GetComponent<PointsCounter>().UpPoints(10);
                        Destroy(gameObject);
                    }
                    else
                    {
                        //environmentObject.GetComponent<PointsCounter>().DownPoints(2);
                    }
                    break;

                case 3:
                    if (collision.collider.name.Contains("Blue"))
                    {
                        pointsCounter.GetComponent<PointsCounter>().UpPoints(10);
                        Destroy(gameObject);
                    }
                    else
                    {
                        pointsCounter.GetComponent<PointsCounter>().DownPoints(5);
                    }
                    break;
                case 0:
                    pointsCounter.GetComponent<PointsCounter>().UpPoints(10);
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
