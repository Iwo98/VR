using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectInteraction : MonoBehaviour
{
    GameCycleWhatsNew gameCycle;

    void Start()
    {
        gameCycle = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameCycleWhatsNew>();
    }

    public void HoverOver()
    {
        
        GetComponent<ClickOnWN>().ClickMe();

    }

    public void HoverEnd()
    {

        GetComponent<ClickOnWN>().resetMaterial();
        GetComponent<ClickOnWN>().setNotSelected();

    }

    public void Activated()
    {
        if (gameCycle.gameState == 3)
            GetComponent<ClickOnWN>().destroyItem();

    }    

}
