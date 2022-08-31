using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balloons
{
    public class PinController : MonoBehaviour
    {
        public int color;
        public GameObject levelSelector;
        private Ray ray;
        private RaycastHit hit;

        public GameObject RHand;

        public void SetColor(int color)
        {
            this.color = color;
            GameObject pinModel = transform.Find("Pin Body").gameObject.transform.Find("Cylinder.001").gameObject;
            pinModel.GetComponent<MeshRenderer>().material = GameObject.FindObjectOfType<ColorManager>().GetMaterial(color);
        }

        public void Button()
        {
            ray = new Ray(RHand.transform.position, RHand.transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Balloon")
                {
                    Balloon balloonHit = hit.transform.gameObject.GetComponent<Balloon>();
                    if (balloonHit.GetColor() == this.color)
                    {
                        GameObject.FindObjectOfType<LevelManager>().NotifyAboutCorrectHit();
                    }
                    else
                    {
                        GameObject.FindObjectOfType<LevelManager>().NotifyAboutIncorrectHit();
                    }
                    balloonHit.GetHit();
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
