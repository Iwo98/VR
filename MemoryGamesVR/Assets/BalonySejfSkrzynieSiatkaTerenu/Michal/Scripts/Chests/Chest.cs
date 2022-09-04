using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chests
{
    public class Chest : MonoBehaviour
    {
        public AudioSource OpenCloseChest;
        public float rotationSpeed = 40;
        public bool opened;
        private const float minAngle = 0.0f;
        private const float maxAngle = 0.9f;
        private UnityEngine.Transform lit;
        // Start is called before the first frame update
        void Start()
        {
            opened = false;
            lit = transform.Find("LitPivot");
        }

        // Update is called once per frame
        void Update()
        {
            if (opened && lit.rotation.x < maxAngle)
            {
                lit.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
            }
            else if (!opened && lit.rotation.x > minAngle)
            {
                lit.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
            }
        }

        public void ChangeState()
        {
            OpenCloseChest.Play();
            opened = !opened;
        }

        public bool IsClosed()
        {
            if (lit.rotation.x <= minAngle && !opened)
            {
                return true;
            }
            return false;
        }
    }
}
