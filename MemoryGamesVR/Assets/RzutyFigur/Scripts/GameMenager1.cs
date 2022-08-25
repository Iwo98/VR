using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenager1 : MonoBehaviour
{
    public List<GenerateFigure1> generators;
    public PlaneFigure figurePlaner;

    //private int randomGoodAnswer;
    public int score = 0;

    void Start()
    {
        Text textObject = GameObject.Find("Text").GetComponent<Text>();
        textObject.text = ";)";
        BeginGame();


    }

    void Update()
    {

    }
    int correctAns;
    private void BeginGame()
    {
        int n = 3;
        int[,,] matrix = new int[n, n, n];
        int[,,] matrixCorrect = new int[n, n, n];
        correctAns = Random.Range(0, 3);
        int zero = 0;
        Debug.Log(correctAns);
       // Debug.Log("r"+randomGoodAnswer);
        for (int i = 0; i < generators.Count; i++)
        {
                for (int x = 0; x < n; x++)
                    for (int z = 0; z < n; z++)
                        for (int y = 0; y < n; y++)
                        {
                            matrix[x, y, z] = Random.Range(0, 4);
                        }
            Debug.Log(matrix);
            if (i == correctAns)
                for (int x = 0; x < n; x++)
                    for (int z = 0; z < n; z++)
                        for (int y = 0; y < n; y++)
                            matrixCorrect[x, y, z] = matrix[x, y, z];

            generators[i].matrix = matrix;
            generators[i].SendMessage("Generate");
        }
        if (figurePlaner)
        {
            figurePlaner.matrix = matrixCorrect;
            figurePlaner.SendMessage("Generate");
        }
    }

    private void RestartGame()
    {
        StopAllCoroutines();

        BeginGame();
    }
    public Text textObject;

    public void ChooseAnswer(int number)
    {
        Debug.Log("wykonuje siê");
        if (number == correctAns)
        {
            textObject.text = "dobra odpowiedz :)";
            Debug.Log("dobrze!!!!!!!");
            score++;
            Debug.Log(score);
        }
        else
            textObject.text = "z³a odpowiedz :(";

    }
}
