using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balloons
{
    public class Sign : MonoBehaviour
    {
        private TMPro.TextMeshPro pointsText;
        private TMPro.TextMeshPro wrongText;
        private TMPro.TextMeshPro timeText;

        // Start is called before the first frame update
        void Start()
        {
            pointsText = transform.Find("Points").GetComponent<TMPro.TextMeshPro>();
            pointsText.text = "0/0";
            wrongText = transform.Find("Wrong").GetComponent<TMPro.TextMeshPro>();
            wrongText.text = "(0)";
            timeText = transform.Find("Time").GetComponent<TMPro.TextMeshPro>();
            timeText.text = "00:00";
        }

        public void AssignPointsText(string points)
        {
            pointsText.text = points;
        }

        public void AssignWrongText(string wrong)
        {
            wrongText.text = wrong;
        }

        public void AssignTimeText(string time)
        {
            timeText.text = time;
        }
    }
}

