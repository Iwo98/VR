using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int game_mode = 7;
    public static bool timer_running;
    public float time_remaining = 30;
    public static int game_state;
    public GameObject canvasScore;
    public GameObject timers;
    public TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
            game_mode = PlayerPrefs.GetInt("curr_game_difficulty");
        timer_running = true;
        game_state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer_running && game_state == 1)
        {
            if (time_remaining > 0)
            {
                time_remaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                time_remaining = 0;
                timer_running = false;
                game_state = 2;
                canvasScore.SetActive(true);
                scoreText.text = ScoreScript.score.ToString() + "/" + ScoreScript.maxScore.ToString() + " PKT";
            }
            for (int i = 0; i < 4; i++)
            {
                TextMeshPro text = timers.transform.GetChild(i).gameObject.GetComponent<TextMeshPro>();
                text.text = GetTime(time_remaining);
            }
        }
        if(game_state == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                TextMeshPro text = timers.transform.GetChild(i).gameObject.GetComponent<TextMeshPro>();
                text.text = "00:00";
            }
        }
    }

    string GetTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void startGame()
    {
        timer_running = true;
        game_state = 1;
    }

    public void ExitToMenu()
    {
        float score = 0;
        if (ScoreScript.maxScore > 0)
        {
            score = ScoreScript.score / ScoreScript.maxScore * 100;
        } //  Change for difficulty
        GameChoiceManager gameManager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        gameManager.endGameManagement(score);
    }
}
