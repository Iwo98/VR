using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    public static int score;
    public static int maxScore;
    public static GameObject currentGameObject;
    // Start is called before the first frame update
    void Start()
    {
        currentGameObject = GameObject.Find("ScoreBoards"); ;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void scoreUp()
    {
        score++;
        maxScore++;
        for (int i = 0; i < 4; i++) {
            TextMeshPro text = currentGameObject.transform.GetChild(i).gameObject.GetComponent<TextMeshPro>();
            text.text = score.ToString() + "/" + maxScore.ToString();
        }
    }

    public static void scoreDown()
    {
        maxScore++;
        for (int i = 0; i < 4; i++) {
            TextMeshPro text = currentGameObject.transform.GetChild(i).gameObject.GetComponent<TextMeshPro>();
            text.text = score.ToString() + "/" + maxScore.ToString();
        }
    }


}
