using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balloons
{
    public class Balloon : MonoBehaviour
    {
        public float xLowerLimit;
        public float xUpperLimit;
        public float speed;
        private float lifeTime;
        private float direction = 1;
        private float heightModifier;
        private int color;

        // Start is called before the first frame update
        void Start()
        {
            heightModifier = Random.Range(1.0f, 5.0f);
            GameObject ballonBody = transform.Find("Balloon Body").gameObject;
            ballonBody.GetComponent<MeshRenderer>().material = GameObject.FindObjectOfType<ColorManager>().GetMaterial(color);
            lifeTime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            lifeTime += Time.deltaTime;

            float height = Mathf.Sin(lifeTime * speed) * heightModifier;
            transform.position = new Vector3(transform.position.x + direction * speed * Time.deltaTime, height, transform.position.z);

            if (transform.position.x < xLowerLimit || transform.position.x > xUpperLimit)
            {
                Destroy(gameObject);
            }
        }

        public void SetDirection(int direction)
        {
            this.direction = direction;
        }

        public void SetColor(int color)
        {
            this.color = color;
        }

        public void SetSpeed(int speed)
        {
            this.speed = speed;
        }

        public int GetColor()
        {
            return color;
        }

        public void GetHit()
        {
            Destroy(gameObject);
        }
    }
}
