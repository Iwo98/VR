using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCollide : MonoBehaviour
{
    public float speed = 1f;
    public void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "BucketEnd")
        {
            Debug.Log("BUss");
            this.gameObject.SetActive(false);
        }
    }
}
