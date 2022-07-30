using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameExampleLevel : MonoBehaviour
{
    public int difficulty;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Witaj");
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
        {
            difficulty = PlayerPrefs.GetInt("curr_game_difficulty");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
            game_manager.endGameManagement(score);
        }
    }
}
