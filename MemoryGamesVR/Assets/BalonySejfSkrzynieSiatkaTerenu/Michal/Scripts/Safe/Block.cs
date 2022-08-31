using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Safe {
    public class Block : MonoBehaviour
    {
        public Material Light, Dark;
        private bool blinking;
        private int activeBlocks;
        private float blinkingCd;
        private const float blinkingInterval = 5.0f;
        private int digit;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (blinking)
            {
                blinkingCd -= Time.deltaTime;
                if (blinkingCd <= 0.0f)
                {
                    blinkingCd = blinkingInterval;
                    for (int i = 1; i <= 9; i++)
                    {
                        GameObject window = transform.Find("Window" + i).gameObject;
                        window.GetComponent<MeshRenderer>().material = Dark;
                    }
                    LightWindows(digit);
                }
            }
        }

        public void Init()
        {
            blinkingCd = Random.Range(1.0f, 3.0f);
            var difficultyData = GameObject.FindObjectOfType<LevelManager>().GetLevelDifficulty();
            blinking = difficultyData.windowBlinking;
            activeBlocks = difficultyData.blocks;

            if (gameObject.name == "Block5")
            {
                if (activeBlocks != 5)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void LightWindows(int numberOfWindows)
        {
            digit = numberOfWindows;
            List<int> windowIds = new List<int>();
            for (int i = 0; i < numberOfWindows; i++)
            {
                bool next = false;
                while (!next)
                {
                    int id = Random.Range(1, 10);
                    if (!windowIds.Contains(id))
                    {
                        windowIds.Add(id);
                        next = true;
                    }
                }
            }

            foreach (var windowId in windowIds)
            {
                GameObject window = transform.Find("Window" + windowId).gameObject;
                window.GetComponent<MeshRenderer>().material = Light;
            }
        }
    }
}