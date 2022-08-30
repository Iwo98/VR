using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class ChooseTrainingRoutineCanvasLogicv2 : MonoBehaviour
{
    public Canvas ChooseCognitiveGamesCanvas, ChooseExerciseGamesCanvas;
    public GameObject selectDifficulty;
    public GameObject cognitiveGameNameTemplate, cognitiveOrderOptionsTemplate;
    public GameObject exerciseGameNameTemplate, exerciseOrderOptionsTemplate;
    public List<GameObject> cognitiveGamesOrder = new List<GameObject>();
    public List<GameObject> exerciseGamesOrder = new List<GameObject>();
    public GamesControllerv2 gamesController = new GamesControllerv2();
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
            populateCognitiveGameNames();
            populateCognitiveOrderOptions();
            populateExerciseGameNames();
            populateExercisOrderOptions();
        }

    }

    public void populateCognitiveOrderOptions()
    {
        ToggleGroup toggleGroupTemplate = findToggleGroup(cognitiveOrderOptionsTemplate);
        Toggle templateToggle = findToggle(cognitiveOrderOptionsTemplate);

        int index = 0;
        foreach (string gameName in game_values.cognitiveGameNames)
        {
            Toggle toggle;
            toggle = Instantiate(templateToggle, toggleGroupTemplate.transform, true);
            toggle.transform.SetParent(toggleGroupTemplate.transform);
            toggle.transform.Translate(0, -0.16f * index, 0);
            toggle.GetComponentInChildren<Text>().text = gameName;

            index++;
        }

        Destroy(templateToggle.gameObject);

        cognitiveGamesOrder.Add(cognitiveOrderOptionsTemplate);
        for (int i = 0; i < game_values.cognitiveTrainingNumberOfGames; i++)
        {
            GameObject orderComponent;
            orderComponent = Instantiate(cognitiveOrderOptionsTemplate, ChooseCognitiveGamesCanvas.transform, true);
            orderComponent.transform.SetParent(ChooseCognitiveGamesCanvas.transform);
            orderComponent.transform.Translate(0.25f * i, 0, 0);
            orderComponent.GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();

            if (i == 0)
            {
                Destroy(orderComponent);
            }
            else
            {
                cognitiveGamesOrder.Add(orderComponent);
            }
        }
    }

    public void populateExercisOrderOptions()
    {
        ToggleGroup toggleGroupTemplate = findToggleGroup(exerciseOrderOptionsTemplate);
        Toggle templateToggle = findToggle(exerciseOrderOptionsTemplate);

        int index = 0;
        foreach (string gameName in game_values.exerciseGameNames)
        {
            Toggle toggle;
            toggle = Instantiate(templateToggle, toggleGroupTemplate.transform, true);
            toggle.transform.SetParent(toggleGroupTemplate.transform);
            toggle.transform.Translate(0, -0.16f * index, 0);
            toggle.GetComponentInChildren<Text>().text = gameName;

            index++;
        }

        Destroy(templateToggle.gameObject);

        exerciseGamesOrder.Add(exerciseOrderOptionsTemplate);
        for (int i = 0; i < game_values.exerciseTrainingNumberOfGames; i++)
        {
            GameObject orderComponent;
            orderComponent = Instantiate(exerciseOrderOptionsTemplate, ChooseExerciseGamesCanvas.transform, true);
            orderComponent.transform.SetParent(ChooseExerciseGamesCanvas.transform);
            orderComponent.transform.Translate(0.25f * i, 0, 0);
            orderComponent.GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();

            if (i == 0)
            {
                Destroy(orderComponent);
            }
            else
            {
                exerciseGamesOrder.Add(orderComponent);
            }
        }
    }

    public void populateCognitiveGameNames()
    {
        int index = 0;
        foreach (string gameName in game_values.cognitiveGameNames)
        {
            GameObject game;
            game = Instantiate(cognitiveGameNameTemplate, ChooseCognitiveGamesCanvas.transform, true);
            game.transform.SetParent(ChooseCognitiveGamesCanvas.transform);
            game.transform.Translate(0, -0.16f * index, 0);

            TMP_Text gameNameText = findText(game);
            Image gameImage = findImage(game);

            gameNameText.text = gameName;
            gameImage.sprite = Resources.Load<Sprite>(game_values.gameIcons2DPaths[index]);

            index++;
        }
        Destroy(cognitiveGameNameTemplate);
    }

    public void populateExerciseGameNames()
    {
        int index = 0;
        foreach (string gameName in game_values.exerciseGameNames)
        {
            GameObject game;
            game = Instantiate(exerciseGameNameTemplate, ChooseExerciseGamesCanvas.transform, true);
            game.transform.SetParent(ChooseExerciseGamesCanvas.transform);
            game.transform.Translate(0, -0.16f * index, 0);

            TMP_Text gameNameText = findText(game);
            Image gameImage = findImage(game);

            gameNameText.text = gameName;
            gameImage.sprite = Resources.Load<Sprite>(game_values.gameIcons2DPaths[index]);

            index++;
        }
        Destroy(exerciseGameNameTemplate);
    }

    public void handleNextButton()
    {
        chooseCognitiveGames();
        ChooseCognitiveGamesCanvas.gameObject.SetActive(false);
        ChooseExerciseGamesCanvas.gameObject.SetActive(true);
        saveUserDifficultyData();
    }


    public void handlePlayButton()
    {
        chooseExerciseGames();
        ChooseExerciseGamesCanvas.gameObject.SetActive(false);
        gamesController.mergeGames(game_values.trainingNumberOfGames);
        gamesController.printMergedGames("prepared");
        StartTraining();
    }

    private void chooseCognitiveGames()
    {
        List<RawGamev2> rawGames = new List<RawGamev2>();
        int index = 0;

        Debug.Log(cognitiveGamesOrder.Count);
        foreach (GameObject cognitiveGame in cognitiveGamesOrder)
        {
            RawGamev2 rawGame = new RawGamev2();
            ToggleGroup toggleGroup = findToggleGroup(cognitiveGame);
            Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();

            rawGame.name = toggle.GetComponentInChildren<Text>().text;
            rawGame.difficulty = difficulty;
            rawGames.Add(rawGame);

            index++;
        }
        gamesController.setCognitiveRawGames(rawGames);
        gamesController.prepareCognitiveGamesList(game_values.gameNames);
        //gamesController.printGames("prepared");
    }

    private void chooseExerciseGames()
    {
        List<RawGamev2> rawGames = new List<RawGamev2>();
        int index = 0;

        Debug.Log(cognitiveGamesOrder.Count);
        foreach (GameObject exerciseGame in exerciseGamesOrder)
        {
            RawGamev2 rawGame = new RawGamev2();
            ToggleGroup toggleGroup = findToggleGroup(exerciseGame);
            Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();

            rawGame.name = toggle.GetComponentInChildren<Text>().text;
            rawGame.difficulty = difficulty;
            rawGames.Add(rawGame);

            index++;
        }
        gamesController.setExerciseRawGames(rawGames);
        gamesController.prepareExerciseGamesList(game_values.gameNames);
        //gamesController.printExerciseGames("prepared");
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
        foreach (PreparedGamev2 game in gamesController.mergedGamesList)
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


    private void setDifficultySelect()
    {
        TMP_Dropdown select = findSelect(selectDifficulty);

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

public class RawGamev2
{
    public string name;
    public int difficulty;
}

public class PreparedGamev2
{
    public int id;
    public int difficulty;
}

public class GamesControllerv2
{
    public List<RawGamev2> rawCognitiveGamesList = new List<RawGamev2>();
    public List<PreparedGamev2> preparedCognitiveGamesList = new List<PreparedGamev2>();

    public List<RawGamev2> rawExerciseGamesGamesList = new List<RawGamev2>();
    public List<PreparedGamev2> preparedExerciseGamesList = new List<PreparedGamev2>();

    public List<PreparedGamev2> mergedGamesList = new List<PreparedGamev2>();

    public void prepareCognitiveGamesList (List<string> allGames)
    {
        foreach (RawGamev2 game in rawCognitiveGamesList)
        {
            PreparedGamev2 preparedGame = new PreparedGamev2();
            preparedGame.id = allGames.FindIndex(a => a.Contains(game.name));
            preparedGame.difficulty = game.difficulty;
            preparedCognitiveGamesList.Add(preparedGame);
        }
    }

    public void prepareExerciseGamesList(List<string> allGames)
    {
        foreach (RawGamev2 game in rawExerciseGamesGamesList)
        {
            PreparedGamev2 preparedGame = new PreparedGamev2();
            preparedGame.id = allGames.FindIndex(a => a.Contains(game.name));
            preparedGame.difficulty = game.difficulty;
            preparedExerciseGamesList.Add(preparedGame);
        }
    }

    public void setCognitiveRawGames(List<RawGamev2> newGames)
    {
        rawCognitiveGamesList = newGames;
    }

    public void setExerciseRawGames(List<RawGamev2> newGames)
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
            foreach (RawGamev2 game in rawCognitiveGamesList)
            {
                Debug.Log("Gra: " + game.name + " i ma poziom trudnoœci: " + game.difficulty);
            }
        }

        if (type == "prepared")
        {
            foreach (PreparedGamev2 game in preparedCognitiveGamesList)
            {
                Debug.Log("Gra: " + game.id + " ma poziom trudnoœci: " + game.difficulty);
            }
        }
    }

    public void printExerciseGames(string type)
    {
        if (type == "notPrepared")
        {
            foreach (RawGamev2 game in rawExerciseGamesGamesList)
            {
                Debug.Log("Gra: " + game.name + " i ma poziom trudnoœci: " + game.difficulty);
            }
        }

        if (type == "prepared")
        {
            foreach (PreparedGamev2 game in preparedExerciseGamesList)
            {
                Debug.Log("Gra: " + game.id + " ma poziom trudnoœci: " + game.difficulty);
            }
        }
    }

    public void printMergedGames(string type)
    {
        if (type == "prepared")
        {
            int index = 0;
            foreach (PreparedGamev2 game in mergedGamesList)
            {
                Debug.Log("Kolejnoœæ: " + index + " Gra nr: " + game.id + " ma poziom trudnoœci: " + game.difficulty);
            }
        }
    }
}


