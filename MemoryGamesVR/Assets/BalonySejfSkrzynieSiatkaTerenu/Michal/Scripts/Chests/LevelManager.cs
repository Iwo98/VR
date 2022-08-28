using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chests
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject[] findingPrefabs;
        public ResultSign resultSign;
        public ScoreSign scoreSign;
        public TimeSign timeSign;
        public Chest[] chests;
        public int difficulty = 0;
        public Vector3 heightOffset;
        private LevelDifficulty[] difficulties;
        private List<Object> spawnedFindings;
        private List<int> spawnedFindingsIds;
        private int correctAnswer;
        private float timePassed = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            correctAnswer = -1;
            spawnedFindings = new List<Object>();
            spawnedFindingsIds = new List<int>();

            difficulties = new LevelDifficulty[] {
                new LevelDifficulty {
                    chests = 3,
                    upperFindingsRange = 10
                },
                new LevelDifficulty {
                    chests = 4,
                    upperFindingsRange = 10
                },
                new LevelDifficulty {
                    chests = 5,
                    upperFindingsRange = 10
                },
                new LevelDifficulty {
                    chests = 5,
                    upperFindingsRange = 8
                },
                new LevelDifficulty {
                    chests = 5,
                    upperFindingsRange = 6
                },
            };

            Time.timeScale = 0.0f;
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.timeScale == 0.0f)
            {
                return;
            }
            
            if (chests[0].IsClosed())
            {
                GenerateFindings();
            }

            timePassed += Time.deltaTime;
            string seconds = (int)(30 - timePassed) >= 10 ? ((int)(30 - timePassed)).ToString() : "0" + ((int)(30 - timePassed)).ToString();
            timeSign.SetTime("00:" + seconds);
            if (timePassed >= 30)
            {
                // GAME OVER
                Debug.Log("GAME OVER");
            }
        }

        public void SelectChest(char chestId)
        {
            if (correctAnswer == -1)
            {
                correctAnswer = Random.Range(0, GetLevelDifficulty().chests);
                ChangeChestsState();
                return;
            }

            if (int.Parse(chestId.ToString()) - 1 == correctAnswer)
            {
                resultSign.SetCorrectMessage();
                scoreSign.CorrectAnswer();
            }
            else
            {
                resultSign.SetIncorrectMessage();
                scoreSign.IncorrectAnswer();
            }
            ChangeChestsState();
        }

        public LevelDifficulty GetLevelDifficulty()
        {
            return difficulties[difficulty];
        }

        public void GenerateFindings()
        {
            // usuń narysowane obiekty
            while (spawnedFindings.Count > 0)
            {
                Destroy(spawnedFindings[0]);
                spawnedFindings.RemoveAt(0);
            }

            // roszada IDs
            var rng = new System.Random();
            spawnedFindingsIds = spawnedFindingsIds.OrderBy(item => rng.Next()).ToList();

            // wylosuj <0; liczba_skrzynek> = x <- correctAnswer
            correctAnswer = Random.Range(0, GetLevelDifficulty().chests);

            // wylosuj nowe ID
            int newFinding;
            while(true)
            {
                newFinding = Random.Range(0, GetLevelDifficulty().upperFindingsRange);
                if (!spawnedFindingsIds.Contains(newFinding))
                {
                    // spawnedFindingsIds.Add(newFinding);
                    break;
                }
            }

            // zamień x z IDs
            spawnedFindingsIds[correctAnswer] = newFinding;
            
            for (int i = 0; i < spawnedFindingsIds.Count; i++)
            {
                var spawned = Instantiate(findingPrefabs[spawnedFindingsIds[i]], chests[i].transform.position + heightOffset, findingPrefabs[spawnedFindingsIds[i]].transform.rotation);
                spawnedFindings.Add(spawned);
            }
            
            ChangeChestsState();
        }

        public void SetLevelDifficulty(int level)
        {
            difficulty = level;
            Time.timeScale = 1.0f;
            for (int i = 4; i >= difficulties[difficulty].chests; i--)
            {
                chests[i].gameObject.SetActive(false);
            }
            InitializeFindings();
        }

        private void InitializeFindings()
        {
            for (int i = 0; i < GetLevelDifficulty().chests; i++)
            {
                int newFinding = Random.Range(0, GetLevelDifficulty().upperFindingsRange);
                if (spawnedFindingsIds.Contains(newFinding))
                {
                    i--;
                    continue;
                }
                spawnedFindingsIds.Add(newFinding);
            }

            for (int i = 0; i < spawnedFindingsIds.Count; i++)
            {
                var spawned = Instantiate(findingPrefabs[spawnedFindingsIds[i]], chests[i].transform.position + heightOffset, findingPrefabs[spawnedFindingsIds[i]].transform.rotation);
                spawnedFindings.Add(spawned);
            }

            ChangeChestsState();
        }

        private void ChangeChestsState()
        {
            for (int i = 0; i < GetLevelDifficulty().chests; i++)
            {
                chests[i].ChangeState();
            }
        }
    }

    public class LevelDifficulty
    {
        public int chests;
        public int upperFindingsRange;
    }
}
