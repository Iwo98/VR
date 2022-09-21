using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class Game3 : MonoBehaviour
{

    public enum Game_states
    {
        No_game,
        Remember_toys,
        Restart_toys,
        Setting_toys,
        End_game
    }
    public List<GameObject> toys = new List<GameObject>();
    public int levelNumber = 8;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI infoLevelText;
    public GameObject laver;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject win_sound;
    public GameObject mistake_sound;
    public TextMeshProUGUI buttonText;
    public GameChoiceManager game_manager;

    private List<GameObject> gameToys = new List<GameObject>();
    private List<int> randListNumbers = new List<int>();
    private List<GameObject> addToys = new List<GameObject>();
    private List<GameObject> startToys = new List<GameObject>();
    private List<GameObject> gameToysSort = new List<GameObject>();


    private List<GameObject> createKnobsWithoutSnapzones = new List<GameObject>();
    
    private float timer = 0;
    private Game_states game = Game_states.No_game;
    private static int MAX_knobs = 8;
    private int[] correctKnobs = new int[MAX_knobs];
    private int[] setKnobs = new int[MAX_knobs];
    private int timeEndRememberKnobs = 10;
    private int timeEndGame = 30;
    private int difficulty = 0;
    private int score = 0;
    private int fails = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
        {
            difficulty = PlayerPrefs.GetInt("curr_game_difficulty");
            if (difficulty < 3)
            {
                difficulty = 5;
            }
            else if (difficulty < 5 && difficulty >= 3)
            {
                difficulty = 6;
            }
            else if (difficulty < 7 && difficulty >= 5)
            {
                difficulty = 7;
            }
            else difficulty = 9;

        }

    }
    
    public int selectLevel()
    {
        float rotLaver = laver.transform.rotation.z;
        int level = 5;
        if (rotLaver <= 0.5f && rotLaver > 0.25f)
            level = 5;
        else if (rotLaver <= 0.25f && rotLaver >= 0)
            level = 6;
        else if (rotLaver <= 0 && rotLaver > -0.25f)
            level = 7;
        else if (rotLaver <= -0.25f && rotLaver >= -0.5f)
            level = 9;

        return level;
    }
    public void infoLevel()
    {
        int level = difficulty;
        if (level == 5)
        {
            infoLevelText.text = "£atwy";
        }
        else if (level == 6)
        {
            infoLevelText.text = "Normalny";
        }
        else if (level == 7)
        {
            infoLevelText.text = "Trudny";
        }
        else if (level == 9)
        {
            infoLevelText.text = "Ekstremalny";
        }
    }
    public void GameMenager()
    {
        if (game == Game_states.No_game)
        {
            starGame3();
        }
        else if (game == Game_states.Setting_toys)
        {
            checkToys();
        }
        else if (game == Game_states.End_game)
        {
            game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
            game_manager.endGameManagement(score);
        }
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void nextGame()
    {
        SceneManager.LoadScene("Scenes/Game1/Game1_1.0");
    }
    void activationDeactivationHand()
    {
        var left = leftHand.GetComponent<Collider>();
        var right = rightHand.GetComponent<Collider>();
        left.enabled = !left.enabled;
        right.enabled = !right.enabled;
    }

    public void starGame3()
    {
        levelNumber = difficulty;
        infoText.text = "Zapamietaj przedmioty\n na stole masz 10s";
        activationDeactivationHand();
        if (game == 0)
        {
            game = Game_states.Remember_toys;
            float Xpos = 3.5f;
            float Zpos = 5.0f;
            float Ypos = 0.9372351f;
            float step = 0.2f;
            GameObject newToy;
            int randNumber;
            for (int i = 0; i < levelNumber; i++)
            {
                do
                {
                    randNumber = Random.Range(0, toys.Count);
                } while (randListNumbers.Contains(randNumber));
                randListNumbers.Add(randNumber);
            }
            for (int i = 0; i < levelNumber; i++)
            {
                newToy = Instantiate(toys[randListNumbers[i]], new Vector3(Xpos, Ypos, Zpos + i * step), Quaternion.identity);
                gameToys.Add(newToy);
                startToys.Add(newToy);
            }
        }
    }
    public void updateTimer()
    {
        timer += Time.deltaTime;
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void restartToys()
    {
        game = Game_states.Restart_toys;
        activationDeactivationHand();
        GameObject newToy;
        int randNumber;
        for (int i = 0; i < levelNumber/3; i++)
        {
            do
            {
                randNumber = Random.Range(0, toys.Count);
            } while (randListNumbers.Contains(randNumber));
            randListNumbers.Add(randNumber);
            newToy = Instantiate(toys[randListNumbers[randListNumbers.Count - 1]], new Vector3(0, 0, 0), Quaternion.identity);
            gameToys.Add(newToy);
            addToys.Add(newToy);
        }

        sortGameFigures();
        float Xpos = 3.25f;
        float Zpos = 5.0f;
        float Ypos = 0.9372351f;
        float step = 0.25f;
        int counter = 0;
        int halfNumberToysSort = gameToysSort.Count / 2;
        for (int i = 0; i < halfNumberToysSort; i++)
        {
            gameToysSort[i].transform.position = new Vector3(Xpos, Ypos, Zpos + counter * step);
            counter++;
        }
        Xpos = 3.7f;
        Zpos = 5.0f;
        counter = 0;
        for (int i = halfNumberToysSort; i < gameToysSort.Count; i++)
        {
            gameToysSort[i].transform.position = new Vector3(Xpos, Ypos, Zpos + counter * step);
            counter++;
        }
    }

    public void sortGameFigures()
    {
        gameToysSort = gameToys.OrderBy(tile => tile.name).ToList();
    }
    
     public void settingToys()
     {
         game = Game_states.Setting_toys;
         infoText.text = "Znajdü dodane przedmioty\n i ustaw je w wyznaczonym miejscu masz 20s";
         buttonText.text = "Sprawdü";
     }
    
     public void checkToys()
     {
         game = Game_states.End_game;
         var win = win_sound.GetComponent<AudioSource>();
         var mistake = mistake_sound.GetComponent<AudioSource>();
         bool check = true;
         float Xpos = 2.964076f;
         float Ypos = 0.9544753f;
         float Zpos1 = 5.737184f;
         float Zpos2 = 5.476884f;
         float Zpos3 = 5.222184f;
         Vector3 AddToy1 = new Vector3(Xpos, Ypos, Zpos1);
         Vector3 AddToy2 = new Vector3(Xpos, Ypos, Zpos2);
         Vector3 AddToy3 = new Vector3(Xpos, Ypos, Zpos3);
         for (int i = 0; i < addToys.Count; i++)
         {
             if (addToys[i].transform.position != AddToy1 && addToys[i].transform.position != AddToy2 && addToys[i].transform.position != AddToy3)
             {
                 infoText.text = "Koniec gry\nZle ustawienie";
                 mistake.Play();
                 check = false;
                 break;
             }
         }
         for (int i = 0; i < startToys.Count; i++)
         {
             if (startToys[i].transform.position == AddToy1 || startToys[i].transform.position == AddToy2 || startToys[i].transform.position == AddToy3)
             {
                 //infoText.text = "Koniec gry\nZle ustawienie";
                 mistake.Play();
                 check = false;
                 fails++;
                //break;
            }
         }
         if (check == true)
         {
             //infoText.text = "Koniec gry\nPoprawne ustawienie";
             win.Play();
             //score = 100;
         }
         buttonText.text = "Dalej";
         score = (difficulty - fails) * 100 / difficulty;
         infoText.text = score.ToString() + "%";
    }
    
    // Update is called once per frame
    void Update()
    {
        if (game != Game_states.No_game && game != Game_states.End_game)
        {
            updateTimer();
        }
        int seconds = (int)(timer % 60);
        if (seconds >= timeEndRememberKnobs && game == Game_states.Remember_toys)
        {
            restartToys();
            settingToys();
        }
        if (seconds >= timeEndGame && game == Game_states.Setting_toys)
        {
            checkToys();
        }
    }
}




