using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChooseTrainingRoutineCanvasLogic : MonoBehaviour
{
    public Canvas ChooseGamesCanvas;
    public GameObject gameTemplate, selectDifficulty;
    public GameObject ErrorText;
    public List<GameObject> cognitiveGames = new List<GameObject>();
    public GamesController gamesController = new GamesController();
    public int numberOfCognitiveGames = 8;
    public bool isOrderUnique = true;
    public int difficulty;

    private ConstantGameValues game_values;

    // Start is called before the first frame update
    void Start()
    {
        int isAfterTraining = PlayerPrefs.GetInt("after_training");

        if (isAfterTraining == 1) {
            ChooseGamesCanvas.gameObject.SetActive(false);
        }
        else
        {
       
            ChooseGamesCanvas.gameObject.SetActive(true);
            game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
            game_values.initAllValues();

            setOrderSelect();
            setDifficultySelect();
            populateCognitiveGames();
        }

    }

    private void setOrderSelect()
    {
        TMP_Dropdown select = findSelect(gameTemplate);

        select.options.Clear();

        List<string> selectOptions = new List<string>();

        for (int i = 0; i < numberOfCognitiveGames; i++)
        {
            selectOptions.Add((i + 1).ToString());
        }

        selectOptions.Add("Brak");

        foreach (var option in selectOptions)
        {
            select.options.Add(new TMP_Dropdown.OptionData() { text = option });
        }
    }

    private void setDifficultySelect()
    {
        TMP_Dropdown select = findSelect(selectDifficulty);

        select.options.Clear();

        List<string> selectOptions = new List<string>();

        for (int i = 1; i <= 10; i++)
        {
            selectOptions.Add((i).ToString());
        }

        foreach (var option in selectOptions)
        {
            select.options.Add(new TMP_Dropdown.OptionData() { text = option });
        }


        // Load difficulty from users settings if possibles
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        int load_result = user_data.LoadFile();
        if (load_result == 0) // Load succeced
        {
            select.value = user_data.data.lastChosenDifficulty - 1;
        }

        else
        {
            handleDifficultySelectChange(select);
        }
        

        select.onValueChanged.AddListener(delegate { handleDifficultySelectChange(select); });
    }



    public void populateCognitiveGames()
    {
        int index = 0;
        foreach (string gameName in game_values.gameNames)
        {
            GameObject game;
            game = Instantiate(gameTemplate, ChooseGamesCanvas.transform, true);
            game.transform.SetParent(ChooseGamesCanvas.transform);
            game.transform.Translate(0, -0.15f * index, 0);

            TMP_Dropdown select = findSelect(game);
            TMP_Text gameNameText = findText(game);
            Image gameImage = findImage(game);

            gameNameText.text = gameName;
            gameImage.sprite = Resources.Load<Sprite>(game_values.gameIcons2DPaths[index]);
            select.onValueChanged.AddListener(delegate { handleSelectChange(select); });
            select.value = select.options.Count - 1;

            index++;
            cognitiveGames.Add(game);
        }
        Destroy(gameTemplate);
    }

    public void handleButton()
    {
        validateGames();
        isOrderUnique = gamesController.isGamesOrderUnique;
        if (isOrderUnique)
        {
            ErrorText.SetActive(false);
            saveUserDifficultyData();
            StartTraining();
        } else
        {
            ErrorText.SetActive(true);
        }
    }

    private void validateGames()
    {
        List<RawGame> rawGames = new List<RawGame>();
        foreach (GameObject game in cognitiveGames)
        {
            RawGame rawGame = new RawGame();
            TMP_Dropdown select = findSelect(game);
            TMP_Text gameNameText = findText(game);

            rawGame.order = select.value;
            rawGame.name = gameNameText.text;
            rawGame.difficulty = difficulty;

            if(rawGame.order != select.options.Count - 1)
            {
                rawGames.Add(rawGame);
            }
        }

        gamesController.setRawGames(rawGames);
        gamesController.sortRawGames();
        gamesController.checkIfOrderIsUnique();
        if (gamesController.checkIfOrderIsUnique())
        {
            gamesController.prepareGamesList(game_values.gameNames);
            ChooseGamesCanvas.gameObject.SetActive(false);
        }
    }

    public void StartTraining()
    {
        clearPrefs();
        GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();

        PlayerPrefs.SetInt("is_training", 1);
        PlayerPrefs.SetInt("number_of_games_in_training", gamesController.preparedGamesList.Count);
        int index = 0;
        foreach (PreparedGame game in gamesController.preparedGamesList)
        {
            PlayerPrefs.SetInt("game_id_" + index.ToString(), game.id);
            PlayerPrefs.SetInt("game_difficulty_" + index.ToString(), game.difficulty);
            index++;
        }

        user_data.SaveFile();
        game_manager.chooseNextGame();
    }

    public void saveUserDifficultyData()
    {
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();
        user_data.data.lastChosenDifficulty = difficulty;
        user_data.SaveFile();
    }

    public void handleSelectChange(TMP_Dropdown select)
    {
        int index = select.value;
    }

    public void handleDifficultySelectChange(TMP_Dropdown select)
    {
        difficulty = select.value + 1;
    }

    private TMP_Dropdown findSelect(GameObject gameObject)
    {
        Transform gameTransform = gameObject.GetComponent<Transform>();
        Transform selectComponent = gameTransform.Find("Select");
        TMP_Dropdown select = selectComponent.GetComponent<TMP_Dropdown>();

        return select;
    }

    private TMP_Text findText(GameObject gameObject)
    {
        Transform gameTransform = gameObject.GetComponent<Transform>();
        Transform gameNameComponent = gameTransform.Find("GameName");
        TMP_Text gameNameText = gameNameComponent.GetComponent<TMP_Text>();

        return gameNameText;
    }

    private Image findImage(GameObject gameObject)
    {
        Transform gameTransform = gameObject.GetComponent<Transform>();
        Transform gameImageComponent = gameTransform.Find("GameImage");
        Image gameImage = gameImageComponent.GetComponent<Image>();

        return gameImage;
    }

    private void clearPrefs()
    {
        string username = PlayerPrefs.GetString("username");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("curr_game_num", 0);
    }
}

public class RawGame
{
    public string name;
    public int difficulty;
    public int order;
}

public class PreparedGame
{
    public int id;
    public int difficulty;
}

public class GamesController
{
    public List<RawGame> rawGamesList = new List<RawGame>();
    public List<PreparedGame> preparedGamesList = new List<PreparedGame>();
    public bool isGamesOrderUnique = true;

    public void prepareGamesList (List<string> allGames)
    {
        foreach (RawGame game in rawGamesList)
        {
            PreparedGame preparedGame = new PreparedGame();
            preparedGame.id = allGames.FindIndex(a => a.Contains(game.name));
            preparedGame.difficulty = game.difficulty;
            preparedGamesList.Add(preparedGame);
        }
    }
    public void sortRawGames()
    {
        rawGamesList.Sort((x, y) => x.order.CompareTo(y.order));
    }

    public void setRawGames(List<RawGame> newGames)
    {
        rawGamesList = newGames;
    }

    public bool checkIfOrderIsUnique()
    {
        foreach (RawGame game in rawGamesList)
        {
            int currentOrder = game.order;
            foreach (RawGame gameToCheck in rawGamesList)
            {
                if(gameToCheck.name != game.name)
                {
                    if(currentOrder == gameToCheck.order)
                    {
                        isGamesOrderUnique = false;
                        return false;
                    }
                }
            }
        }

        isGamesOrderUnique = true;
        return true;
    }

    public void printGames(string type)
    {
        if (type == "notPrepared")
        {
            foreach (RawGame game in rawGamesList)
            {
                Debug.Log("Gra: " + game.name + " jest w kolejnoœci: " + game.order + " i ma poziom trudnoœci: " + game.difficulty);
            }
        }

        if (type == "prepared")
        {
            foreach (PreparedGame game in preparedGamesList)
            {
                Debug.Log("Gra: " + game.id + " ma poziom trudnoœci: " + game.difficulty);
            }
        }

    }
}


