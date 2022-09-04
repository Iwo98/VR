using Chests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandlerChests : MonoBehaviour
{
    public Canvas StartMenuCanvas;
    public LevelManager myMain;
    public GameChoiceManager game_manager;
    //public Canvas shader;


    public void ClickStarButton()
    {
        StartMenuCanvas.gameObject.SetActive(false);
        myMain.phase = 1;
        //Debug.Log(myMain.phase);
        //shader.gameObject.SetActive(true);
    }
    public void ClickEndButton()
    {
        game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        game_manager.endGameManagement(myMain.score);
    }
}
