using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandlerPlank : MonoBehaviour
{

    public main myMain;
    public GameChoiceManager game_manager;
    public void ClickEndButton()
    {
        game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        game_manager.endGameManagement(myMain.finalScore);
    }

}
