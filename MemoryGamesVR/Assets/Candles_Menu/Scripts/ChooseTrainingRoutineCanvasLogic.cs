using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class ChooseTrainingRoutineCanvasLogic : MonoBehaviour
{
    public Canvas ChooseCognitiveGamesCanvas, ChooseExerciseGamesCanvas;
    public GameObject selectDifficulty;
    public GameObject cognitiveGameNameTemplate, cognitiveOrderOptionsTemplate;
    public GameObject exerciseGameNameTemplate, exerciseOrderOptionsTemplate;
    public List<GameObject> cognitiveGamesOrder = new List<GameObject>();
    public List<GameObject> exerciseGamesOrder = new List<GameObject>();
    public GamesController gamesController = new GamesController();
    public int difficulty;

    private ConstantGameValues game_values;

    // Start is called before the first frame update
    void Start()
    {
        int isAfterTraining = PlayerPrefs.GetInt("after_training");

        if (isAfterTraining == 1) {
            ChooseCognitiveGamesCanvas.gameObject.SetActive(false);
        }
        else
        {
            ChooseCognitiveGamesCanvas.gameObject.SetActive(true);
            game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
            game_values.initAllValues();

            setDifficultySelect();
            populateGameNames(game_values.cognitiveGameNames, cognitiveGameNameTemplate, ChooseCognitiveGamesCanvas);
            populateOrderOptions(cognitiveOrderOptionsTemplate, game_values.cognitiveGameNames, game_values.cognitiveTrainingNumberOfGames, ChooseCognitiveGamesCanvas, "cognitive");
            populateGameNames(game_values.exerciseGameNames, exerciseGameNameTemplate, ChooseExerciseGamesCanvas);
            populateOrderOptions(exerciseOrderOptionsTemplate, game_values.exerciseGameNames, game_values.exerciseTrainingNumberOfGames, ChooseExerciseGamesCanvas, "exercise");
        }

    }
    public void populateOrderOptions(GameObject orderOptionsTemplate, List<string> gameNames, int numberOfGames, Canvas ChooseGamesCanvas, string typeOfGames )
    {
        ToggleGroup toggleGroupTemplate = findNestedComponent<ToggleGroup>(orderOptionsTemplate, "OrderOptions");
        Toggle templateToggle = findToggle(orderOptionsTemplate);

        int index = 0;
        foreach (string gameName in gameNames)
        {
            Toggle toggle;
            toggle = Instantiate(templateToggle, toggleGroupTemplate.transform, true);
            toggle.transform.SetParent(toggleGroupTemplate.transform);
            toggle.transform.Translate(0, -0.16f * index, 0);
            toggle.GetComponentInChildren<Text>().text = gameName;

            index++;
        }

        Destroy(templateToggle.gameObject);

        if (typeOfGames == "cognitive")
        {
            cognitiveGamesOrder.Add(orderOptionsTemplate);
        } else if (typeOfGames == "exercise")     
        {
            exerciseGamesOrder.Add(orderOptionsTemplate);
        }

        for (int i = 0; i < numberOfGames; i++)
        {
            GameObject orderComponent;
            orderComponent = Instantiate(orderOptionsTemplate, ChooseGamesCanvas.transform, true);
            orderComponent.transform.SetParent(ChooseGamesCanvas.transform);
            orderComponent.transform.Translate(0.25f * i, 0, 0);
            orderComponent.GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();
            Toggle toggle = findToggle(orderComponent);
            toggle.isOn = true;

            if (i == 0)
            {
                Destroy(orderComponent);
            }
            else
            {
                if (typeOfGames == "cognitive")
                {
                    cognitiveGamesOrder.Add(orderComponent);
                }
                else if (typeOfGames == "exercise")
                {
                    exerciseGamesOrder.Add(orderComponent);
                }
            }
        }
    }

    public void populateGameNames(List<string> gameNames, GameObject gameNameTemplate, Canvas ChooseGameCanvas)
    {
        int index = 0;
        foreach (string gameName in gameNames)
        {
            GameObject game;
            game = Instantiate(gameNameTemplate, ChooseGameCanvas.transform, true);
            game.transform.SetParent(ChooseGameCanvas.transform);
            game.transform.Translate(0, -0.16f * index, 0);

            TMP_Text gameNameText = findNestedComponent<TMP_Text>(game, "GameName");
            Image gameImage = findNestedComponent<Image>(game, "GameImage");

            gameNameText.text = gameName;

            int properIndex = game_values.gameNames.FindIndex(a => a.Contains(gameName));
            gameImage.sprite = Resources.Load<Sprite>(game_values.gameIcons2DPaths[properIndex]);

            index++;
        }
        Destroy(gameNameTemplate);
    }

    public void handleNextButton()
    {
        chooseGames(cognitiveGamesOrder, "cognitive");
        ChooseCognitiveGamesCanvas.gameObject.SetActive(false);
        ChooseExerciseGamesCanvas.gameObject.SetActive(true);
        saveUserDifficultyData();
    }

    public void handlePlayButton()
    {
        chooseGames(exerciseGamesOrder, "exercise");
        ChooseExerciseGamesCanvas.gameObject.SetActive(false);
        gamesController.mergeGames(game_values.trainingNumberOfGames);
        gamesController.printMergedGames();
        StartTraining();
    }

    private void chooseGames(List<GameObject> gamesOrder, string gamesType)
    {
        List<RawGame> rawGames = new List<RawGame>();
        int index = 0;

        foreach (GameObject game in gamesOrder)
        {
            RawGame rawGame = new RawGame();
            ToggleGroup toggleGroup = findNestedComponent<ToggleGroup>(game, "OrderOptions");
            Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();

            rawGame.name = toggle.GetComponentInChildren<Text>().text;
            rawGame.difficulty = difficulty;
            rawGames.Add(rawGame);

            index++;
        }
        if (gamesType == "cognitive")
        {
            gamesController.setCognitiveRawGames(rawGames);
            gamesController.prepareCognitiveGamesList(game_values.gameNames);
        }
        else if(gamesType == "exercise")
        {
            gamesController.setExerciseRawGames(rawGames);
            gamesController.prepareExerciseGamesList(game_values.gameNames);
        }

    }

    public void StartTraining()
    {
        clearPrefs();
        GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();

        PlayerPrefs.SetInt("is_training", 1);
        PlayerPrefs.SetInt("number_of_games_in_training", game_values.trainingNumberOfGames);
        int index = 0;
        foreach (PreparedGame game in gamesController.mergedGamesList)
        {
            PlayerPrefs.SetInt("game_id_" + index.ToString(), game.id);
            PlayerPrefs.SetInt("game_difficulty_" + index.ToString(), game.difficulty);
            index++;
        }

        user_data.SaveFile();
        game_manager.chooseNextGame();
    }

    private T findNestedComponent<T>(GameObject gameObject, string ComponentGameObjectName)
    {
        Transform gameObjectTransform = gameObject.GetComponent<Transform>();
        Transform ComponentGameObject = gameObjectTransform.Find(ComponentGameObjectName);
        T gameNameText = ComponentGameObject.GetComponent<T>();

        return gameNameText;
    }

    private Toggle findToggle(GameObject gameObject)
    {
        Transform gameObjectTransform = gameObject.GetComponent<Transform>();
        Transform toggleGroupTransform = gameObjectTransform.Find("OrderOptions");
        Transform toggleComponent = toggleGroupTransform.Find("OrderOption");
        Toggle toggle = toggleComponent.GetComponent<Toggle>();

        return toggle;
    }

    private ToggleGroup findToggleGroup(GameObject gameObject)
    {
        Transform templateTransform = gameObject.GetComponent<Transform>();
        Transform toggleGroupComponent = templateTransform.Find("OrderOptions");
        ToggleGroup toggleGroup = toggleGroupComponent.GetComponent<ToggleGroup>();

        return toggleGroup;
    }

    public void handleDifficultySelectChange(TMP_Dropdown select)
    {
        difficulty = select.value + 1;
    }

    public void saveUserDifficultyData()
    {
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();
        user_data.data.lastChosenDifficulty = difficulty;
        user_data.SaveFile();
    }

    private void setDifficultySelect()
    {
        TMP_Dropdown select = findNestedComponent<TMP_Dropdown>(selectDifficulty, "Select");

        select.options.Clear();

        List<string> selectOptions = new List<string>();

        for (int i = 1; i <= game_values.maxDifficulty; i++)
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
            handleDifficultySelectChange(select);
        }
        else
        {
            handleDifficultySelectChange(select);
        }

        select.onValueChanged.AddListener(delegate { handleDifficultySelectChange(select); });
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
}

public class PreparedGame
{
    public int id;
    public int difficulty;
}

public class GamesController
{
    public List<RawGame> rawCognitiveGamesList = new List<RawGame>();
    public List<PreparedGame> preparedCognitiveGamesList = new List<PreparedGame>();

    public List<RawGame> rawExerciseGamesGamesList = new List<RawGame>();
    public List<PreparedGame> preparedExerciseGamesList = new List<PreparedGame>();

    public List<PreparedGame> mergedGamesList = new List<PreparedGame>();

    public void prepareCognitiveGamesList (List<string> allGames)
    {
        foreach (RawGame game in rawCognitiveGamesList)
        {
            PreparedGame preparedGame = new PreparedGame();
            preparedGame.id = allGames.FindIndex(a => a.Contains(game.name));
            preparedGame.difficulty = game.difficulty;
            preparedCognitiveGamesList.Add(preparedGame);
        }
    }

    public void prepareExerciseGamesList(List<string> allGames)
    {
        foreach (RawGame game in rawExerciseGamesGamesList)
        {
            PreparedGame preparedGame = new PreparedGame();
            preparedGame.id = allGames.FindIndex(a => a.Contains(game.name));
            preparedGame.difficulty = game.difficulty;
            preparedExerciseGamesList.Add(preparedGame);
        }
    }

    public void setCognitiveRawGames(List<RawGame> newGames)
    {
        rawCognitiveGamesList = newGames;
    }

    public void setExerciseRawGames(List<RawGame> newGames)
    {
        rawExerciseGamesGamesList = newGames;
    }

    public void mergeGames(int numberOfGames)
    {
        int exerciseGamesIndex = 0;
        int cognitiveGamesIndex = 0;
        for (int i = 0; i < numberOfGames; i++)
        {
            if (i % 2 == 0)
            {
                mergedGamesList.Add(preparedCognitiveGamesList[cognitiveGamesIndex]);
                cognitiveGamesIndex++;
            }
            else
            {
                mergedGamesList.Add(preparedExerciseGamesList[exerciseGamesIndex]);
                exerciseGamesIndex++;
            }
        }
    }

    public void printGames(string type)
    {
        if (type == "notPrepared")
        {
            foreach (RawGame game in rawCognitiveGamesList)
            {
                Debug.Log("Gra: " + game.name + " i ma poziom trudnoœci: " + game.difficulty);
            }
        }

        if (type == "prepared")
        {
            foreach (PreparedGame game in preparedCognitiveGamesList)
            {
                Debug.Log("Gra: " + game.id + " ma poziom trudnoœci: " + game.difficulty);
            }
        }
    }

    public void printExerciseGames(string type)
    {
        if (type == "notPrepared")
        {
            foreach (RawGame game in rawExerciseGamesGamesList)
            {
                Debug.Log("Gra: " + game.name + " i ma poziom trudnoœci: " + game.difficulty);
            }
        }

        if (type == "prepared")
        {
            foreach (PreparedGame game in preparedExerciseGamesList)
            {
                Debug.Log("Gra: " + game.id + " ma poziom trudnoœci: " + game.difficulty);
            }
        }
    }

    public void printMergedGames()
    {
        int index = 0;
        foreach (PreparedGame game in mergedGamesList)
        {
            Debug.Log("Kolejnoœæ: " + index + " Gra nr: " + game.id + " ma poziom trudnoœci: " + game.difficulty);
            index++;
        }     
    }
}


