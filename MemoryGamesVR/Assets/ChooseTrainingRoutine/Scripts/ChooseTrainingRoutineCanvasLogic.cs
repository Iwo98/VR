using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseTrainingRoutineCanvasLogic : MonoBehaviour
{

    public Canvas ChooseGamesCanvas;
    public GameObject gameTemplate;
    public GameObject ErrorText;
    public List<GameObject> cognitiveGames = new List<GameObject>();
    public GameController gameController = new GameController();
    public int numberOfCognitiveGames = 8;
    public bool isOrderUnique = true;

    private ConstantGameValues game_values;

    // Start is called before the first frame update
    void Start()
    {

        game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
        game_values.initAllValues();

        setSelect();
        populateCognitiveGames();
    }

    private void setSelect()
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

            gameNameText.text = gameName;
            select.onValueChanged.AddListener(delegate { handleSelectChange(select); });
            select.value = select.options.Count - 1;

            index++;
            cognitiveGames.Add(game);
        }
        Destroy(gameTemplate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleButton()
    {
        validateGamesOrder();
        isOrderUnique = gameController.areGamesUnique;
        if (isOrderUnique)
        {
            ErrorText.SetActive(false);
        } else
        {
            ErrorText.SetActive(true);
        }
    }

    private void validateGamesOrder()
    {
        List<GameValues> gamesWithValues = new List<GameValues>();
        foreach (GameObject game in cognitiveGames)
        {
            GameValues gameValues = new GameValues();
            TMP_Dropdown select = findSelect(game);
            TMP_Text gameNameText = findText(game);

            gameValues.id = "id";
            gameValues.order = select.value;
            gameValues.name = gameNameText.text;
            gameValues.difficulty = 2;

            if(gameValues.order != select.options.Count - 1)
            {
                gamesWithValues.Add(gameValues);
            }
        }

        gameController.setGames(gamesWithValues);
        gameController.sortGames();
        gameController.printGames();
        gameController.checkIfOrdersAreUnique();
    }

    public void handleSelectChange(TMP_Dropdown select)
    {
        int index = select.value;
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
}

public class GameValues
{
    public string name;
    public string id;
    public int difficulty;
    public int order;
}

public class GameController
{
    public List<GameValues> games = new List<GameValues>();
    public bool areGamesUnique = true;

    public void sortGames()
    {
        games.Sort((x, y) => x.order.CompareTo(y.order));
    }

    public void setGames(List<GameValues> newGames)
    {
        games = newGames;
    }

    public bool checkIfOrdersAreUnique()
    {
        foreach (GameValues game in games)
        {
            int currentOrder = game.order;
            foreach (GameValues gameToCheck in games)
            {
                if(gameToCheck.name != game.name)
                {
                    if(currentOrder == gameToCheck.order)
                    {
                        areGamesUnique = false;
                        return false;
                    }
                }
            }
        }

        areGamesUnique = true;
        return true;
    }

    public void printGames()
    {
        foreach (GameValues game in games)
        {
            Debug.Log("Gra: " + game.name + " o id: " + game.id +  " jest w kolejnoœci: " + game.order + " i ma poziom trudnoœci: " + game.difficulty);
        }
    }
}


