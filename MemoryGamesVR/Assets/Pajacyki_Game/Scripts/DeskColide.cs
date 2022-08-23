using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskColide : MonoBehaviour
{
    public bool isPlateInDeskArea = false;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "GamePlate")
        {
            isPlateInDeskArea = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "GamePlate")
        {
            isPlateInDeskArea = false;
        }
    }
}
