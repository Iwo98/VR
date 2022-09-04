using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainButtonHandler : MonoBehaviour
{
    public Canvas StartMenuCanvas;
    public GameLogic myMain;
    public GameChoiceManager game_manager;
    public GameObject buttonY;
    public GameObject buttonN;

    public void ClickStarButton()
    {
        StartMenuCanvas.gameObject.SetActive(false);
        buttonY.SetActive(true);
        buttonN.SetActive(true);
        myMain.phase = 1;
        //Debug.Log(myMain.phase);
    }
    public void ClickEndButton()
    {
        game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        game_manager.endGameManagement(myMain.score);
    }
}
