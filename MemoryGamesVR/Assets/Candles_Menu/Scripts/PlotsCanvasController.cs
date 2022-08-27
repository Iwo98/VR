using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlotsCanvasController : MonoBehaviour
{
    private ConstantGameValues game_values;
    private RectTransform graphContainer;
    private int currChartId = 0;
    private float plotSize = 2.0f;
    private int plotMaxGamesShown = 15;

    [SerializeField] 
    private GameObject usernameText, gameNameText, numGamesText, difficultyText, lastScoreText, avgScoreText;
    [SerializeField]
    private GameObject gameIconImg;
    [SerializeField]
    private GameObject scoreAxisText, gamesAxisText;
    [SerializeField]
    private Sprite circleSprite;

    // Start is called before the first frame update
    void Start()
    {
        game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        updateCanvas();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeCurrPlotId(int val)
    {
        currChartId += val;
        int maxVal = game_values.numberOfGames + 1;
        if (currChartId >= maxVal)
        {
            currChartId = maxVal - 1;
        }
        else if (currChartId < 0)
        {
            currChartId = 0;
        }
        updateCanvas();
    }

    public void updateCanvas()
    {
        string username = PlayerPrefs.GetString("username");
        UserData user_data = GameObject.FindObjectsOfType<UserData>()[0];
        user_data.LoadFile();
        List<float> scores = user_data.data.gameScores[currChartId].currGameScores;
        string game_name = "Trening";
        string game_difficulty = "-";
        if (currChartId > 0)
        {
            game_name = game_values.gameNames[currChartId - 1];
            //game_difficulty = user_data.data.getGameDifficulty(currChartId - 1).ToString();

        }
        int games_num = scores.Count;

        usernameText.GetComponent<TextMeshProUGUI>().text = "Statystyki gracza:\n" + username.ToUpper();
        gameNameText.GetComponent<TextMeshProUGUI>().text = game_name;
        numGamesText.GetComponent<TextMeshProUGUI>().text = "Liczba gier: " + games_num.ToString();
        difficultyText.GetComponent<TextMeshProUGUI>().text = "Poziom trudności: " + game_difficulty;
        if (games_num > 0)
        {
            float last_score = scores[games_num - 1];
            float avg_score = 0;
            for (int i = 0; i < games_num; i++)
            {
                avg_score += scores[i];
            }
            avg_score /= games_num;
            lastScoreText.GetComponent<TextMeshProUGUI>().text = "Ostatni wynik: " + ((int)last_score).ToString();
            avgScoreText.GetComponent<TextMeshProUGUI>().text = "Średni wynik: " + ((int)avg_score).ToString();
        }
        else
        {
            lastScoreText.GetComponent<TextMeshProUGUI>().text = "Ostatni wynik: -";
            avgScoreText.GetComponent<TextMeshProUGUI>().text = "Średni wynik: -";
        }

        if (currChartId == 0)
        {
            gameIconImg.SetActive(false);
        }
        else
        {
            gameIconImg.SetActive(true);
            gameIconImg.GetComponent<Image>().sprite = Resources.Load<Sprite>(game_values.gameIcons2DPaths[currChartId - 1]);
        }

        // randomowe score do testowania wykresów <--------------------------
        /*scores = new List<float>();
        int rand_len = UnityEngine.Random.Range(5, 20);
        for(int i = 0; i < rand_len; i++)
        {
            scores.Add(UnityEngine.Random.value * 2000);
        }*/
        // koniec

        List<int> plotScoreList = new List<int>();
        int start_id = 0;
        if(scores.Count > 15)
        {
            start_id = scores.Count - 15;
        }

        float score_max = 0;
        for(int i = start_id; i < scores.Count; i++)
        {
            plotScoreList.Add((int)scores[i]);
            if(scores[i] > score_max)
            {
                score_max = scores[i];
            }
        }

        float val_temp = score_max % 100;
        if(val_temp > 0)
        {
            val_temp = 1;
        }
        score_max = ((int)(score_max / 100) + val_temp) * 100;
        scoreAxisText.GetComponent<TextMeshProUGUI>().text = score_max.ToString();

        if (plotScoreList.Count == 0)
        {
            gamesAxisText.GetComponent<TextMeshProUGUI>().text = "Brak danych";
        }
        else if(plotScoreList.Count == 1)
        {
            gamesAxisText.GetComponent<TextMeshProUGUI>().text = "Ostatnia gra";
        }
        else if(plotScoreList.Count < 5)
        {
            gamesAxisText.GetComponent<TextMeshProUGUI>().text = "Ostatnie " + plotScoreList.Count + " gry";
        }
        else
        {
            gamesAxisText.GetComponent<TextMeshProUGUI>().text = "Ostatnie " + plotScoreList.Count + " gier";
        }


        ShowGraph(plotScoreList, score_max);
    }

    private void ShowGraph(List<int> valueList, float scoreMax)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float xDelta = graphWidth / plotMaxGamesShown;
        float yMaximum = scoreMax;

        // Destroy last graph
        foreach (Transform child in graphContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Plot new graph
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {

            float xPosition = i * xDelta;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.GetComponent<Image>().color = new Color(0.7f, 0, 0, 1f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(plotSize, plotSize);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(0.7f, 0, 0, 1f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, plotSize / 2);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
