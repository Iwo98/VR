using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChooseGameController : MonoBehaviour
{ 
    private ConstantGameValues gameValues;
    private int state = 0;
    private int diff = 1;
    public TMP_Text gameText;
    public TMP_Text diffText;
    public GameObject[] gameIcons;



    // Start is called before the first frame update
    void Start()
    {
        gameValues = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
        if (PlayerPrefs.HasKey("game_id_0"))
        {
            state = PlayerPrefs.GetInt("game_id_0");
            diff = PlayerPrefs.GetInt("game_difficulty_0");
        }
        StateChange(0);
        DifficultyChange(0);
    }

    // Update is called once per frame
    void Update()
    {
        //StateChange(0);
    }

    public void StateChange(int direction)
    {
        int numGames = gameValues.numberOfGames;
        state += direction;
        if (state < 0)
            state = numGames - 1;
        if (state > numGames - 1)
            state = 0;

        string title = gameValues.gameNames[state];

        ActivateIcon();
        gameText.text = title;
    }

    public void DifficultyChange(int direction)
    {
        int numDiffs = gameValues.maxDifficulty;
        diff += direction;
        if (diff < 1)
            diff = numDiffs;
        if (diff > numDiffs)
            diff = 1;

        string diffString = "Poziom trudności: " + diff + "/10";

        diffText.text = diffString;

        PlayerPrefs.SetInt("game_difficulty_0", diff);
    }

    public void PlayGame()
    {
        clearPrefs();
        PlayerPrefs.SetInt("game_id_0", state);
        PlayerPrefs.SetInt("game_difficulty_0", diff);
        GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        game_manager.chooseNextGame();
    }

    public void ExitToMenu()
    {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Candles_Menu/Scenes/Menu"));
    }

    private void ActivateIcon()
    {
        foreach (GameObject item in gameIcons)
        {
            item.SetActive(false);
        }
        try
        {
            gameIcons[state].SetActive(true);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private void clearPrefs()
    {
        string username = PlayerPrefs.GetString("username");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("is_training", 0);
        PlayerPrefs.SetInt("curr_game_num", 0);
    }
}
