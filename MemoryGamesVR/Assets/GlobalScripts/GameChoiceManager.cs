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

    public void chooseNextGame()
    {
        int currGameNum = PlayerPrefs.GetInt("curr_game_num");
        if (PlayerPrefs.GetInt("is_training") == 0 && currGameNum >= 1)
        {
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Candles_Menu/Scenes/WyborGry"));
        }
        else if (PlayerPrefs.GetInt("is_warm_up") == 1 && currGameNum < game_values.warmUpNumberOfGames)
        {
            int currGameId = PlayerPrefs.GetInt("warm_up_game_id_" + currGameNum.ToString());
            string scenePath = game_values.gameScenes[currGameId];
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, scenePath));
        }
        else if (PlayerPrefs.GetInt("is_warm_up") == 1 && currGameNum >= game_values.warmUpNumberOfGames)
        {
            PlayerPrefs.SetInt("is_warm_up", 0);
            PlayerPrefs.SetInt("curr_game_num", 0);
            chooseNextGame();
        }
        else if (PlayerPrefs.GetInt("is_training") == 1 && PlayerPrefs.GetInt("is_relax") == 0 && currGameNum >= game_values.trainingNumberOfGames)
        {
            PlayerPrefs.SetInt("after_training", 1);
            PlayerPrefs.SetInt("is_relax", 1);
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, game_values.relaxScene));
        }
        else if (PlayerPrefs.GetInt("is_relax") == 1)
        {
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Candles_Menu/Scenes/ModulTreningowy"));
        }
        else
        {
            int currGameId = PlayerPrefs.GetInt("game_id_" + currGameNum.ToString());
            int currGameDifficulty = PlayerPrefs.GetInt("game_difficulty_" + currGameNum.ToString());
            PlayerPrefs.SetInt("curr_game_difficulty", currGameDifficulty);
            string scenePath = game_values.gameScenes[currGameId];
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, scenePath));
        }
    }

    public void endGameManagement(float score)
    {   
        int currGameNum = PlayerPrefs.GetInt("curr_game_num");
        if (PlayerPrefs.GetInt("is_training") == 1)
        {
            PlayerPrefs.SetFloat("game_score_" + currGameNum.ToString(), score);
        }
        PlayerPrefs.SetInt("curr_game_num", currGameNum + 1);

        chooseNextGame();
    }

    public void startTraining(List<PreparedGame> games)
    {
        GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();

        PlayerPrefs.SetInt("is_training", 1);
        int index = 0;
        foreach (PreparedGame game in games)
        {
            PlayerPrefs.SetInt("game_id_" + index.ToString(), game.id);
            PlayerPrefs.SetInt("game_difficulty_" + index.ToString(), game.difficulty);
            index++;
        }

        user_data.SaveFile();
        game_manager.chooseNextGame();
    }

    public void prepareWarmUp(List<PreparedGame> games)
    {
        clearPrefs();
        PlayerPrefs.SetInt("is_warm_up", 1);
        PlayerPrefs.SetInt("is_relax", 0);
        int index = 0;
        foreach (PreparedGame game in games)
        {
            PlayerPrefs.SetInt("warm_up_game_id_" + index.ToString(), game.id);
            index++;
        }
    }

    private void clearPrefs()
    {
        string username = PlayerPrefs.GetString("username");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("curr_game_num", 0);
    }
}
