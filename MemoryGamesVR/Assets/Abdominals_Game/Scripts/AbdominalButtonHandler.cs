using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbdominalButtonHandler : MonoBehaviour
{
    public Canvas StartMenuCanvas;
    public MainAbdominals myMain;


    public void ClickStarButton()
    {
        StartMenuCanvas.gameObject.SetActive(false);
        myMain.phase = 1;
        //Debug.Log(myMain.phase);
    }
}

