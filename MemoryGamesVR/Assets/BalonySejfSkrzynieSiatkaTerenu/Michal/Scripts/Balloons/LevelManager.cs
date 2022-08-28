using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Balloons
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject balloonPrefab;
        public Vector3 balloonSpawnLocation;
        public PinController leftPin;
        public PinController rightPin;
        public Sign sign;
        public int difficulty = 0;

        private float balloonSpawnInterval = 5.0f;
        private float timePassed = 0.0f;
        private float balloonSpawnCd = 0.0f;
        private int leftPinColor;
        private int rightPinColor;
        private int totalBalloonsToBeHit = 0;
        private int correctBalloonsHit = 0;
        private int incorrectBalloonsHit = 0;
        private LevelDifficulty[] difficulties;

        // Start is called before the first frame update
        void Start()
        {
            difficulties = new LevelDifficulty[] {
                new LevelDifficulty{
                    balloonSpeed = 3,
                    balloonSpawnInterval = 3.5f,
                    balloonColorLowerRange = 0
                },
                new LevelDifficulty{
                    balloonSpeed = 6,
                    balloonSpawnInterval = 3,
                    balloonColorLowerRange = 0
                },
                new LevelDifficulty{
                    balloonSpeed = 12,
                    balloonSpawnInterval = 2.5f,
                    balloonColorLowerRange = 0
                },
                new LevelDifficulty{
                    balloonSpeed = 12,
                    balloonSpawnInterval = 2,
                    balloonColorLowerRange = 0
                },
                new LevelDifficulty{
                    balloonSpeed = 15,
                    balloonSpawnInterval = 1.5f,
                    balloonColorLowerRange = 7
                }
            };

            Time.timeScale = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            timePassed += Time.deltaTime;
            balloonSpawnCd += Time.deltaTime;
            if (timePassed >= 30)
            {
                // pass the score, compute difficulty level, exit scene
                Time.timeScale = 0f;
            }
            if (balloonSpawnCd >= balloonSpawnInterval)
            {
                SpawnBalloon();
                balloonSpawnCd = 0.0f;
            }

            string seconds = (int)(30 - timePassed) >= 10 ? ((int)(30 - timePassed)).ToString() : "0" + ((int)(30 - timePassed)).ToString();
            sign.AssignTimeText("00:" + seconds);
            sign.AssignPointsText(correctBalloonsHit + "/" + totalBalloonsToBeHit);
            sign.AssignWrongText("(" + incorrectBalloonsHit + ")");
        }

        public void SpawnBalloon()
        {
            int locationVersion = Random.Range(0, 2);
            if (locationVersion == 0)
            {
                locationVersion = -1;
            }
            Vector3 spawnLocation = new Vector3(balloonSpawnLocation.x * locationVersion * (-1), balloonSpawnLocation.y, balloonSpawnLocation.z + locationVersion * 3);
            GameObject balloon = Instantiate(balloonPrefab, spawnLocation , balloonPrefab.transform.rotation);
            int balloonColor = Random.Range(difficulties[difficulty].balloonColorLowerRange, 12);
            balloon.SendMessage("SetDirection", locationVersion);
            balloon.SendMessage("SetColor", balloonColor);
            balloon.SendMessage("SetSpeed", difficulties[difficulty].balloonSpeed);
            if (balloonColor == leftPinColor || balloonColor == rightPinColor)
            {
                totalBalloonsToBeHit++;
            }
        }

        public void NotifyAboutCorrectHit()
        {
            correctBalloonsHit++;
        }

        public void NotifyAboutIncorrectHit()
        {
            incorrectBalloonsHit++;
        }

        public void SetLevelDifficulty(int level)
        {
            difficulty = level;
            Time.timeScale = 1.0f;
            balloonSpawnInterval = difficulties[difficulty].balloonSpawnInterval;
            rightPinColor = Random.Range(difficulties[difficulty].balloonColorLowerRange, 12);
            rightPin.SetColor(rightPinColor);
            leftPinColor = Random.Range(difficulties[difficulty].balloonColorLowerRange, 12);
            leftPin.SetColor(leftPinColor);

            balloonSpawnCd = balloonSpawnInterval;
        }
    }

    public class LevelDifficulty
    {
        public int balloonSpeed;
        public float balloonSpawnInterval;
        public int balloonColorLowerRange;
    }
}
