using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedFlash : MonoBehaviour {

    public GameObject selectedObject;
    public GameObject selectedObjectMouse;
    public int redCol;
    public int greenCol;
    public int blueCol;
    public bool lookingAtObject = false;
    public bool flashingIn = true;
    public bool startedFlashing = false;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(selectedObject.name);
        if (lookingAtObject) {
            selectedObject.GetComponent<Renderer>().material.color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol, 255);
        }

    }

    void OnMouseOver()
    {
        selectedObject = GameObject.Find(CastingToObject.selectedObject);
        selectedObjectMouse = selectedObject;
        lookingAtObject = true;
        if (!startedFlashing){
            startedFlashing = true;
            StartCoroutine(FlashObject());
        }

    }

    void OnMouseExit()
    {
        startedFlashing = false;
        lookingAtObject = false;
        StopCoroutine(FlashObject());
        selectedObjectMouse.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
    }

    IEnumerator FlashObject() {
        while (lookingAtObject) {

            yield return new WaitForSeconds(0.05f);
            if (flashingIn) {
                if (blueCol <= 30)
                    flashingIn = false;
                else {
                    blueCol -= 25;
                    greenCol -= 1;
                }
            }

            if (!flashingIn){
                if (blueCol >= 250)
                    flashingIn = true;
                else {
                    blueCol += 25;
                    greenCol += 1;
                }

            }
        }
    }
}
