using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Safe
{
    public class Car : MonoBehaviour
    {
        public Vector3[] wayPoints;
        private float wayPointRadius = 1;
        private float speed;
        private int numberOfWayPoints;

        private int currentWayPoint = 0;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (IsWayPointReached(wayPoints[currentWayPoint])) // reached wayPoint
            {
                currentWayPoint++;
                if (currentWayPoint == numberOfWayPoints)
                {
                    // GAME'S OVER
                    Time.timeScale = 0.0f;
                    currentWayPoint--;
                }
            }
            Vector3 direction = (wayPoints[currentWayPoint] - transform.position).normalized * Time.deltaTime * speed;
            transform.position += direction;


            // Determine which direction to rotate towards
            Vector3 targetDirection = wayPoints[currentWayPoint] - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = 3.0f * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        public void Init()
        {
            var difficultyData = GameObject.FindObjectOfType<LevelManager>().GetLevelDifficulty();
            speed = difficultyData.carSpeed;
            numberOfWayPoints = difficultyData.wayPoints;
        }

        bool IsWayPointReached(Vector3 wayPoint)
        {
            if (Vector3.Distance(transform.position, wayPoint) < wayPointRadius)
            {
                return true;
            }
            return false;
        }
    }
}
