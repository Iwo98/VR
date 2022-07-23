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
    public GameObject[] trainingResultsGameId;
    public GameObject[] trainingResultsGameText;
    public GameObject[] trainingResultsGameImg;
    public GameObject trainingResultsSumText;
    public GameObject trainingPlotsCanvas;

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
        clearPrefs();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StartTraining()
    {
        clearPrefs();
        GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();
        PlayerPrefs.SetInt("is_training", 1);
        for (int i = 0; i < game_values.trainingNumberOfGames; i++)
        {
            List<int> game_vals = user_data.data.chooseTrainingGame();
            PlayerPrefs.SetInt("game_id_" + i.ToString(), game_vals[0]);
            PlayerPrefs.SetInt("game_difficulty_" + i.ToString(), game_vals[1]);
            Debug.Log(game_vals[1]);
        }
        user_data.SaveFile();
        game_manager.chooseNextGame();
    }

    // Results canvas
    private void displayResultsCanvas()
    {
        GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();
        user_data.data.lastChosenGame = -1;
        float total_score = 0.0f;
        for (int i = 0; i < 8; i++)
        {
            if (i < game_values.trainingNumberOfGames)
            {
                int game_id = PlayerPrefs.GetInt("game_id_" + i.ToString());
                string game_name = game_values.gameNames[game_id];
                int game_diff = PlayerPrefs.GetInt("game_difficulty_" + i.ToString());
                float game_score = PlayerPrefs.GetFloat("game_score_" + i.ToString());
                total_score += game_score;
                trainingResultsGameText[i].GetComponent<TextMeshProUGUI>().text = game_name + "(" + game_diff.ToString() + "): " + game_score.ToString();
                trainingResultsGameImg[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(game_values.gameIcons2DPaths[game_id]);
                trainingResultsGameId[i].SetActive(true);
                trainingResultsGameText[i].SetActive(true);
                trainingResultsGameImg[i].SetActive(true);
                user_data.data.AddScore(game_id + 1, game_score);
            }
            else
            {
                trainingResultsGameId[i].SetActive(false);
                trainingResultsGameText[i].SetActive(false);
                trainingResultsGameImg[i].SetActive(false);
            }
        }
        trainingResultsSumText.GetComponent<TextMeshProUGUI>().text = "Wynik treningu:\n" + total_score.ToString();
        trainingResultsCanvas.SetActive(true);
        user_data.data.AddScore(0, total_score);  // Total score
        user_data.SaveFile();
    }

    public void closeResultsCanvas()
    {
        trainingResultsCanvas.SetActive(false);
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



    // Misc
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void clearPrefs()
    {
        string username = PlayerPrefs.GetString("username");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetInt("curr_game_num", 0);
    }
}
