using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBassSound : MonoBehaviour
{
    public AudioSource BassSound;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "GamePlate")
        {
            BassSound.Play();
        }
    }
}
