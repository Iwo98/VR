using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DentedPixel;
using UnityEngine.UI;

public class CandlesGameController : MonoBehaviour
{

    public int difficulty;
    public int timeToStart;
    public int timeToRemember;
    public int timePlayLength;
    public float timeBetweenScoreIcons;
    private int finalScore;
    public GameObject rememberTimeBar;
    public GameObject playTimeBar;
    public GameObject canvasRememberTimer;
    public GameObject canvasPlayTimer;
    public GameObject canvasScore;
    public GameObject exitToMenuButton;
    public TMP_Text scoreText;
    public TMP_Text finalScoreText;
    public GameObject[] scoreIcons;
    public Sprite goodIcon;
    public Sprite badIcon;
    public GameObject stampAudio;
    public GameObject singleChandelier;
    public GameObject tripleChandelier;
    private Level level;
    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
        {
            difficulty = PlayerPrefs.GetInt("curr_game_difficulty");
        }

        gameStarted = false;
        if (difficulty < 1)
            difficulty = 1;
        if (difficulty > 10)
            difficulty = 10;

        switch (difficulty)
        {
            case 1:
                level = gameObject.AddComponent<Level1>();
                break;
            case 2:
                level = gameObject.AddComponent<Level2>();
                break;
            case 3:
                level = gameObject.AddComponent<Level3>();
                break;
            case 4:
                level = gameObject.AddComponent<Level4>();
                break;
            case 5:
                level = gameObject.AddComponent<Level5>();
                break;
            case 6:
                level = gameObject.AddComponent<Level6>();
                break;
            case 7:
                level = gameObject.AddComponent<Level7>();
                break;
            case 8:
                level = gameObject.AddComponent<Level8>();
                break;
            case 9:
                level = gameObject.AddComponent<Level9>();
                break;
            case 10:
                level = gameObject.AddComponent<Level10>();
                break;
            default:
                level = gameObject.AddComponent<Level1>();
                break;
        }
        level.SpawnCandles(singleChandelier, tripleChandelier);
    }

    public void StartGame()
    {
        canvasRememberTimer.SetActive(true);
        StartCoroutine(RandomizeCandles(timeToStart, level.candleStatesOriginal));
        StartCoroutine(RandomizeCandles(timeToStart + timeToRemember, level.candleStates));
        StartCoroutine(StartGameTimer());
    }

    public void FinishGame()
    {
        canvasScore.SetActive(true);
        gameStarted = false;
        scoreText.text = "";
        finalScoreText.text = "";
        foreach (GameObject icon in scoreIcons)
        {
            icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        StartCoroutine(AnimateScore());
    }

    public void ExitToMenu()
    {
        GameChoiceManager gameManager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        gameManager.endGameManagement(finalScore);
    }

    private IEnumerator AnimateScore()
    {
        int score = level.CheckCandles();
        for (int i = 0; i < level.GetNumberOfCandles(); i++)
        {
            yield return new WaitForSeconds(timeBetweenScoreIcons);
            scoreIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            if (i < score)
            {
                scoreIcons[i].GetComponent<Image>().sprite = goodIcon;
            }
            else
            {
                scoreIcons[i].GetComponent<Image>().sprite = badIcon;
            }
            stampAudio.GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(timeBetweenScoreIcons);
        CalculateFinalScore();
        scoreText.text = score.ToString() + "/" + level.GetNumberOfCandles().ToString();
        finalScoreText.text = "Liczba punktów: " + finalScore.ToString();
        stampAudio.GetComponent<AudioSource>().Play();
        exitToMenuButton.SetActive(true);
    }

    private void CalculateFinalScore()
    {
        //float multiplier = (float)(0.7 + (difficulty * 0.3));
        //finalScore = (int)(1.0f * level.CheckCandles() / level.GetNumberOfCandles() * multiplier * 1300);
        finalScore = Mathf.RoundToInt((level.CheckCandles() * 100 / level.GetNumberOfCandles() ));
        Debug.Log(finalScore);
    }

    private void AnimateRememberBar()
    {
        LeanTween.scaleX(rememberTimeBar, 0, timeToRemember);
    }

    private void AnimatePlayBar()
    {
        LeanTween.scaleX(playTimeBar, 0, timePlayLength);
    }

    public bool GetStatus()
    {
        return gameStarted;
    }

    private IEnumerator StartGameTimer()
    {
        yield return new WaitForSeconds(timeToStart);
        AnimateRememberBar();
        yield return new WaitForSeconds(timeToRemember);
        canvasRememberTimer.SetActive(false);
        canvasPlayTimer.SetActive(true);
        gameStarted = true;
        AnimatePlayBar();
        yield return new WaitForSeconds(timePlayLength);
        canvasPlayTimer.SetActive(false);
        FinishGame();
    }

    private IEnumerator RandomizeCandles(int timeToWait, int[] result)
    {
        yield return new WaitForSeconds(timeToWait);
        level.RandomizeCandles(result);
    }
}
