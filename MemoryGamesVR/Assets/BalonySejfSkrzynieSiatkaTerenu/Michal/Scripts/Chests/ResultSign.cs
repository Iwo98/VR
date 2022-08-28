using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chests
{
    public class ResultSign : MonoBehaviour
    {
        private TMPro.TextMeshPro resultMessage;

        // Start is called before the first frame update
        void Start()
        {
            resultMessage = transform.Find("Result Text").GetComponent<TMPro.TextMeshPro>();
            resultMessage.text = "";
        }

        public void SetCorrectMessage()
        {
            resultMessage.text = "DOBRZE";
        }

        public void SetIncorrectMessage()
        {
            resultMessage.text = "ŹLE";
        }
    }
}

