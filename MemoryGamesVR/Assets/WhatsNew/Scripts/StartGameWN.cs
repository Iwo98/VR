using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameWN : MonoBehaviour
{
    GameCycleWhatsNew gameCycle;

    void Start()
    {        
        gameCycle = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameCycleWhatsNew>();
    }

    public void ChangeStateToOne()
    {
        Debug.Log("Start button pressed");
        gameCycle.StartGame();
    }
}
