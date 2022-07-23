using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorId = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        MainMazeRunner[] mainRunner = Object.FindObjectsOfType<MainMazeRunner>();
        if (mainRunner[0].gamePhase == 0)
        {
            mainRunner[0].UpdatePosition(doorId);
        }        
    }

    public void OnClick()
    {
        MainMazeRunner[] mainRunner = Object.FindObjectsOfType<MainMazeRunner>();
        if (mainRunner[0].gamePhase == 0)
        {
            mainRunner[0].UpdatePosition(doorId);
        }
    }
}
