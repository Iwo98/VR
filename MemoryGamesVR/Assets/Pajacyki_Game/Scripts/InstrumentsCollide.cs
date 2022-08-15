using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentsCollide : MonoBehaviour
{
    public AudioSource InstrumentSound;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Colide")
        {
            Debug.Log("Sound!");
            InstrumentSound.Play();
        }
    }
}
