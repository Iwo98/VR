using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
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
        mainRunner[0].EndGameButton();
    }

    public void OnClick()
    {
        MainMazeRunner[] mainRunner = Object.FindObjectsOfType<MainMazeRunner>();
        mainRunner[0].EndGameButton();
    }
}
