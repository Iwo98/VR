using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public enum Game_states
{
    No_game,
    Remember_tower,
    Destruction_tower,
    Build_tower,
    End_game
}
public class Game1 : MonoBehaviour
{
    public List<GameObject> figures = new List<GameObject>();
    public GameObject snapZones;
    public int levelNumber = 5;
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

    private List<GameObject> gameFigures = new List<GameObject>();
    private List<GameObject> gameFiguresSort = new List<GameObject>();
    private float timer = 0;
    private GameObject snapZonesCopies;
    public Game_states game = Game_states.No_game;
    private float rot;
    private int timeEndRememberTower = 10;
    private int timeStartBuildTower = 11;
    private int timeEndGame = 30;
    private int difficulty = 0;
    private int score = 0;

    void Start()
    {
        snapZonesCopies = Instantiate(snapZones, new Vector3(15.436f, -0.818f, 5.981f), Quaternion.identity);
        snapZonesCopies.SetActive(false);
        
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
        {
            difficulty = PlayerPrefs.GetInt("curr_game_difficulty");
            Debug.Log(difficulty);
            if (difficulty < 3)
            {
                difficulty = 3;
            }
            else if (difficulty < 5 && difficulty >= 3)
            {
                difficulty = 5;
            }
            else if (difficulty < 7 && difficulty >= 5)
            {
                difficulty = 6;
            }
            else difficulty = 7;

        }
        Debug.Log(difficulty);
    }
    public int selectLevel()
    {
        rot = laver.transform.rotation.z;
        int level = 3;
        if (rot <= 0.5f && rot > 0.25f)
            level = 3;
        else if (rot <= 0.25f && rot >= 0)
            level = 5;
        else if (rot <= 0 && rot > -0.25f)
            level = 6;
        else if (rot <= -0.25f && rot >= -0.5f)
            level = 7;

        return level;
    }
    public void infoLevel()
    {
        int level = difficulty;
        if (level == 3)
        {
            infoLevelText.text = "Łatwy";
        }
        else if (level == 5)
        {
            infoLevelText.text = "Normalny";
        }
        else if (level == 6)
        {
            infoLevelText.text = "Trudny";
        }
        else if (level == 7)
        {
            infoLevelText.text = "Ekstremalny";
        }
    }
    public void GameMenager()
    {
        if (game == Game_states.No_game)
        {
            starGame1();
        }
        else if (game == Game_states.Build_tower)
        {
            checkTower();
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
        SceneManager.LoadScene("Scenes/Game2/Game2_1.1");
    }
    void activationDeactivationHand()
    {
        var left = leftHand.GetComponent<Collider>();
        var right = rightHand.GetComponent<Collider>();
        left.enabled = !left.enabled;
        right.enabled = !right.enabled;
    }
    public void starGame1()
    {
        //levelNumber = selectLevel();
        infoText.text = "Zapamietaj ułożenie wieży\n masz 10s";
        activationDeactivationHand();
        if (game == 0)
        {
            game = Game_states.Remember_tower;
            float Xpos = 3.021f;
            float Zpos = 5.23f;
            float Ypos = 0.9372351f;
            float step = 0.1428f;

            List<int> listNumbers = new List<int>();
            int randNumber;
            for (int i = 0; i < levelNumber; i++)
            {
                do
                {
                    randNumber = Random.Range(0, figures.Count);
                } while (listNumbers.Contains(randNumber));
                listNumbers.Add(randNumber); 
            }
            for (int i = 0; i < levelNumber; i++)
            {
                gameFigures.Add(Instantiate(figures[listNumbers[i]], new Vector3(Xpos, Ypos + i * step, Zpos), Quaternion.identity));
            }
        }
    }
    
    public void destructionTower()
    {
        game = Game_states.Destruction_tower;
        activationDeactivationHand();
        sortGameFigures();
        snapZones.SetActive(false);
        float Xpos = 3.3f;
        float Zpos = 4.6f;
        float Ypos = 0.9372351f;
        float step = 0.15f;
        for (int i = 0; i < levelNumber; i++)
        {
            gameFiguresSort[i].transform.position = new Vector3(Xpos, Ypos, Zpos + i * step);
        }
  
    }
    public void sortGameFigures()
    {
       gameFiguresSort = gameFigures.OrderBy(tile => tile.name).ToList();
    }
    public void updateTimer()
    {
        timer += Time.deltaTime;
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void buildTower()
    {  
        game = Game_states.Build_tower;
        infoText.text = "Zbuduj poprzednią wieże\n masz 20s";
        buttonText.text = "Sprawdź";
        snapZonesCopies.SetActive(true);
    }

    public void checkTower()
    {
        game = Game_states.End_game;
        var win = win_sound.GetComponent<AudioSource>();
        var mistake = mistake_sound.GetComponent<AudioSource>();
        bool check = true;
        for (int i = 1; i < levelNumber; i++)
        {
            
            if(gameFigures[i-1].transform.position.y >= gameFigures[i].transform.position.y)
            {
                infoText.text = "Koniec gry\nZla wieża";
                mistake.Play();
                check = false;
                break;
            }
        }
        if (check == true)
        {
            infoText.text = "Koniec gry\nPoprawna wieża";
            win.Play();
            score = 100;
        }
        buttonText.text = "Dalej";
    }

    // Update is called once per frame
    void Update()
    {
        if (game != Game_states.No_game && game != Game_states.End_game)
        {
            updateTimer();
        }
        int seconds = (int)(timer % 60);
        if (seconds >= timeEndRememberTower && game == Game_states.Remember_tower)
        {
            destructionTower();
        }
        if (seconds >= timeStartBuildTower && game == Game_states.Destruction_tower)
        {
            buildTower();
        }
        if (seconds >= timeEndGame && game == Game_states.Build_tower)
        {
            checkTower();
        }
    }
}
