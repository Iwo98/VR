using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaxGameManager : MonoBehaviour
{
    public Canvas StartingCanvas;
    public int phase = 1;
    public float currTime = 0;
    public float maxTime = 5f;


    void Update()
    {
        if (phase == 1)
        {
            StartingCanvas.gameObject.SetActive(true);
        } else if (phase == 2)
        {
            currTime += Time.deltaTime;
            if (currTime > maxTime)
            {
                phase = 3;
            }
        } else if (phase == 3)
        {
            GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
            game_manager.endGameManagement(0);
            Debug.Log("Koniec");
        }
    }

    public void handleButton()
    {
        StartingCanvas.gameObject.SetActive(false);
        phase = 2;
    }
}
