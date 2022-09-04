using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TrainingController : MonoBehaviour
{
    private ConstantGameValues game_values;
    public GameObject trainingResultsCanvas;
    public GameObject gameResultTemplate;
    public GameObject trainingResultsSumText;
    public GameObject trainingPlotsCanvas;
    public TextMeshProUGUI difficultyText;

    // Start is called before the first frame update
    void Start()
    {
        game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
        game_values.initAllValues();  // Inicjalizacja wartości w ConstantGameValues, potrzebne w canvasie wyników trening(bez tego wartości nie inicjalizują się wystarczająco szybko)
        trainingResultsCanvas.SetActive(false);
        if (PlayerPrefs.HasKey("after_training") && PlayerPrefs.GetInt("after_training") == 1)
        {
            displayResultsCanvas();
        }
    }

    // Results canvas
    private void displayResultsCanvas()
    {
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();
        float total_score = 0.0f;
        int difficulty = 0;

        int index = 0;
        for (int i = 0; i < game_values.trainingNumberOfGames; i++)
        {
            int game_id = PlayerPrefs.GetInt("game_id_" + i.ToString());
            string game_name = game_values.gameNames[game_id];
            difficulty = PlayerPrefs.GetInt("game_difficulty_" + i.ToString());
            float game_score = PlayerPrefs.GetFloat("game_score_" + i.ToString());
            total_score += game_score;

            GameObject game;
            game = Instantiate(gameResultTemplate, trainingResultsCanvas.transform, true);
            game.transform.SetParent(trainingResultsCanvas.transform);
            game.transform.Translate(0, -0.16f * index, 0);

            TMP_Text gameName = findText(game, "TextName");
            TMP_Text gameId = findText(game, "TextId");
            Image gameImage = findImage(game);

            gameName.text = game_name + ": " + game_score.ToString();
            gameId.text = (index + 1).ToString() + ".";
            gameImage.sprite = Resources.Load<Sprite>(game_values.gameIcons2DPaths[game_id]);

            index++;
        }
        Destroy(gameResultTemplate);

        difficultyText.text = "Poziom Trudności: " + difficulty.ToString();
        trainingResultsSumText.GetComponent<TextMeshProUGUI>().text = "Wynik treningu:\n" + total_score.ToString();
        trainingResultsCanvas.SetActive(true);
        user_data.data.AddScore(0, total_score);  // Total score
        user_data.SaveFile();
    }

    public void closeResultsCanvas()
    {
        trainingResultsCanvas.SetActive(false);
        clearPrefs();
    }

    // Charts canvas
    public void displayPlotsCanvas()
    {
        trainingPlotsCanvas.SetActive(true);
    }

    public void closePlotsCanvas()
    {
        trainingPlotsCanvas.SetActive(false);
    }

    private void clearPrefs()
    {
        string username = PlayerPrefs.GetString("username");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("curr_game_num", 0);
    }

    // Misc
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private TMP_Text findText(GameObject gameObject, string gameObjectName)
    {
        Transform gameTransform = gameObject.GetComponent<Transform>();
        Transform gameNameComponent = gameTransform.Find(gameObjectName);
        TMP_Text gameNameText = gameNameComponent.GetComponent<TMP_Text>();

        return gameNameText;
    }

    private Image findImage(GameObject gameObject)
    {
        Transform gameTransform = gameObject.GetComponent<Transform>();
        Transform gameImageComponent = gameTransform.Find("ImageGame");
        Image gameImage = gameImageComponent.GetComponent<Image>();

        return gameImage;
    }
}
