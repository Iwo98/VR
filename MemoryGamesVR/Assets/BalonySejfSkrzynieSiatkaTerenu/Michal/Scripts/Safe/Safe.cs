using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Safe
{
    public class Safe : MonoBehaviour
    {
        public string code = "";
        private string correctCode;
        private TMPro.TextMeshPro displayedCode;
        private bool isOpen = false;
        private const int maxCodeLength = 5;
        // Start is called before the first frame update
        void Start()
        {
            displayedCode = transform.Find("CodeText").GetComponent<TMPro.TextMeshPro>();
            displayedCode.text = code;
        }

        // Update is called once per frame
        void Update()
        {
            if (isOpen)
            {
                // game finished
            }
        }

        public void AddDigit(string digit)
        {
            if (code.Length == maxCodeLength)
            {
                return;
            }
            code += digit;
            displayedCode.text = code;
            if (code == correctCode)
            {
                isOpen = true;
                code = "DOBRZE";
                displayedCode.fontSize = 48;
                displayedCode.text = code;
            }
        }

        public void RemoveLastDigit()
        {
            if (code.Length == 0)
            {
                return;
            }
            code = code.Remove(code.Length - 1);
            displayedCode.text = code;
        }

        public void SetCorrectCode(string code)
        {
            correctCode = code;
        }
    }   
}
