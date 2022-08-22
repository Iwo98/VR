using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public Canvas StartMenuCanvas;
    public Main myMain;


    public void ClickStarButton()
    {
        StartMenuCanvas.gameObject.SetActive(false);
        myMain.phase = 1;
        //Debug.Log(myMain.phase);
    }

}
