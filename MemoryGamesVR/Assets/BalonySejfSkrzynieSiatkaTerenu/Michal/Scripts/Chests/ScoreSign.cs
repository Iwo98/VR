using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chests
{
    public class ScoreSign : MonoBehaviour
    {
        private TMPro.TextMeshPro scoreText;
        public int correct = 0;
        public int total = 0;

        // Start is called before the first frame update
        void Start()
        {
            scoreText = transform.Find("Result Text").GetComponent<TMPro.TextMeshPro>();
            scoreText.text = "0/0";
        }

        public void CorrectAnswer()
        {
            correct++;
            IncorrectAnswer();
        }

        public void IncorrectAnswer()
        {
            total++;
            scoreText.text = correct.ToString() + "/" + total.ToString();
        }
    }
}
