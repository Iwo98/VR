using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Safe
{
    public class LevelManager : MonoBehaviour
    {
        public string correctCode;
        public Block[] blocks;
        public int difficulty = 0;
        private LevelDifficulty[] difficulties;

        // Start is called before the first frame update
        void Start()
        {
            difficulties = new LevelDifficulty[]{
                new LevelDifficulty{
                    blocks = 4,
                    carSpeed = 10,
                    wayPoints = 2,
                    windowBlinking = false
                },
                new LevelDifficulty{
                    blocks = 4,
                    carSpeed = 20,
                    wayPoints = 2,
                    windowBlinking = false
                },
                new LevelDifficulty{
                    blocks = 4,
                    carSpeed = 30,
                    wayPoints = 2,
                    windowBlinking = false
                },
                new LevelDifficulty{
                    blocks = 5,
                    carSpeed = 35f,
                    wayPoints = 3,
                    windowBlinking = false
                },
                new LevelDifficulty{
                    blocks = 5,
                    carSpeed = 35,
                    wayPoints = 3,
                    windowBlinking = true
                },
            };

            //Time.timeScale = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public LevelDifficulty GetLevelDifficulty()
        {
            return difficulties[difficulty];
        }

        public void SetLevelDifficulty(int level)
        {
            difficulty = level;

            for (int i = 0; i < difficulties[difficulty].blocks; i++)
            {
                blocks[i].Init();
                int digit = Random.Range(0, 10);
                correctCode += digit;
                blocks[i].LightWindows(digit);
            }

            GameObject.FindObjectOfType<Car>().Init();

            Safe safe = GameObject.FindObjectOfType<Safe>();
            safe.SetCorrectCode(correctCode);

            Time.timeScale = 1.0f;
        }
    }

    public class LevelDifficulty
    {
        public int blocks;
        public int wayPoints;
        public float carSpeed;
        public bool windowBlinking;
    }
}