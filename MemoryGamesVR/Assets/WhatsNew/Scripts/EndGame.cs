using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    GameCycleWhatsNew gameCycle;

    void Start()
    {
        gameCycle = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameCycleWhatsNew>();
    }

    public void FinishGame()
    {   
        Debug.Log("Witamy w przycisku kończenia");
        GameChoiceManager gameManager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        gameManager.endGameManagement(gameCycle.score);
    }
}
