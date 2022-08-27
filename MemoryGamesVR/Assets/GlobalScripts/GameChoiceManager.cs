using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameChoiceManager : MonoBehaviour
{
    private ConstantGameValues game_values;

    // Start is called before the first frame update
    void Start()
    {
        game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void chooseNextGame()
    {
        int currGameNum = PlayerPrefs.GetInt("curr_game_num");
        int numberOfGamesInTraining = PlayerPrefs.GetInt("number_of_games_in_training");
        if (PlayerPrefs.GetInt("is_training") == 0 && currGameNum >= 1)
        {
            //SceneManager.LoadScene("Candles_Menu/Scenes/WyborGry");
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Candles_Menu/Scenes/WyborGry"));
        }
        else if (PlayerPrefs.GetInt("is_training") == 1 && currGameNum >= numberOfGamesInTraining)
        {
            PlayerPrefs.SetInt("after_training", 1);
            //SceneManager.LoadScene("Candles_Menu/Scenes/ModulTreningowy");
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Candles_Menu/Scenes/ModulTreningowy"));
        }
        else
        {
            int currGameId = PlayerPrefs.GetInt("game_id_" + currGameNum.ToString());
            int currGameDifficulty = PlayerPrefs.GetInt("game_difficulty_" + currGameNum.ToString());
            PlayerPrefs.SetInt("curr_game_difficulty", currGameDifficulty);
            string scenePath = game_values.gameScenes[currGameId];
            //SceneManager.LoadScene(scenePath);
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, scenePath));
        }
    }

    public void endGameManagement(float score)
    {   
        int currGameNum = PlayerPrefs.GetInt("curr_game_num");
        PlayerPrefs.SetFloat("game_score_" + currGameNum.ToString(), score);
        PlayerPrefs.SetInt("curr_game_num", currGameNum + 1);
        chooseNextGame();
    }
}
