using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollider : MonoBehaviour
{
    public AudioSource ScoreSound;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "MainCamera")
        {
            Debug.Log("HIT");
            ScoreSound.Play();
            Destroy(this.gameObject, 1.0f);
        }
    }
}
