using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Safe
{
    public class Player : MonoBehaviour
    {
        public Safe safe;
        public GameObject levelSelector;
        private Ray ray;
        private RaycastHit hit;

        public GameObject RHand;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
             
        }

        public void Button()
        {
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ray = new Ray(RHand.transform.position, RHand.transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "SafeButton")
                {
                    char buttonDigit = hit.transform.name[hit.transform.name.Length - 1];
                    if (buttonDigit == 'D')
                    {
                        safe.RemoveLastDigit();
                    }
                    else
                    {
                        safe.AddDigit(buttonDigit.ToString());
                    }
                }
                else if (hit.transform.tag == "LevelButton")
                {
                    int level = int.Parse(hit.transform.name[hit.transform.name.Length - 1].ToString()) - 1;
                    Destroy(levelSelector);
                    GameObject.FindObjectOfType<LevelManager>().SetLevelDifficulty(level);
                }
            }
        }
    }
}
