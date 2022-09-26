using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbdominalButtonHandler : MonoBehaviour
{
    public Canvas StartMenuCanvas;
    public MainAbdominals myMain;
    public GameChoiceManager game_manager;


    public void ClickStarButton()
    {
        StartMenuCanvas.gameObject.SetActive(false);
        myMain.phase = 1;
        //Debug.Log(myMain.phase);
    }

    public void ClickEndButton()
    {
        game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        game_manager.endGameManagement(myMain.finalScore); 
    }
}

