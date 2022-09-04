using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static System.Math;

public class GameLogic : MonoBehaviour
{
    // ========== GAME LOGIC ==========
    // Mesh generators for updates
    public List<MeshGenerator2> meshGenerators;
    // Object to rotate
    public Transform RotationObject;
    // Digit Display
    public NumberDisplay NumberDisplay;
    // Player correct answers
    public int Points = 0;
    // Player incorrect answers
    public int Errors = 0;
    // Answer for actual question
    public bool isActualCorrect = true;
    // Debug
    public bool Regenerate = false;

    public int phase = 0;
    public Canvas StartMenuCanvas;
    public Canvas EndMenuCanvas;
    public TextMeshProUGUI finalText;
    public GameObject buttonY;
    public GameObject buttonN;

    // ========== GAME LEVEL ==========
    // Is noise similar in x and y - false-true
    public bool lvlNoiseSimilar = false;
    // Is terrain moving - false-true
    public bool lvlMove = false;
    // Is table rotated - false-true
    public bool lvlRotation = false;
    // Difficulty - 1.0-10.0
    public float lvlDifficulty = 1.0f;
    public int score = 0;

    // ========== ACTUAL SETTINGS ==========
    private float xShiftSpeed = 0.0f;
    private float zShiftSpeed = 0.0f;

    // ========== INNER SETTINGS ==========
    private float minNoise = 0.1f;
    private float maxNoise = 0.5f;
    private float minShiftSpeed = 0.001f;
    private float maxShiftSpeed = 0.01f;
    private float minHeight = 5.0f;
    private float maxHeight = 10.0f;
    private float dif2;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
        {
            lvlDifficulty = PlayerPrefs.GetInt("curr_game_difficulty");

        }
        GenerateNewQuestion();
    }

    // Update is called once per frame
    void Update()
    {   if (phase == 1)
        {
            
            if (Regenerate)
            {
                Regenerate = false;
                GenerateNewQuestion();
            }


            if (lvlMove)
            {
                foreach (MeshGenerator2 generator in meshGenerators)
                {
                    generator.xNoiseShift += xShiftSpeed;
                    generator.zNoiseShift += zShiftSpeed;
                }
            }

        }
        else if (phase == 2)
        {
            buttonY.SetActive(false);
            buttonN.SetActive(false);
            EndMenuCanvas.gameObject.SetActive(true);
            score = (int)Round((Points - Errors)*100.0/(Points + Errors));
            finalText.text = score.ToString() + "%";
            phase = 3;
        }
    }

    private void GenerateNewQuestion()
    {
        // TMP for record
        if (Points > 1)
            lvlMove = true;
        if (Points > 3)
            lvlRotation = true;


        if (meshGenerators.Count > 0)
        {
            isActualCorrect = Random.Range(0, 2) == 1;
            NewGenerator(meshGenerators[0]);

            if (isActualCorrect)
                for (int i = 1; i < meshGenerators.Count; i++)
                    CopyGenerator(meshGenerators[0], meshGenerators[i]);
            else
                for (int i = 1; i < meshGenerators.Count; i++)
                {
                    CopyGenerator(meshGenerators[0], meshGenerators[i]);
                    ModifyGeneartor(meshGenerators[i], lvlDifficulty);
                }
            
            if (lvlMove)
            {
                xShiftSpeed = Random.Range(minShiftSpeed, maxShiftSpeed);
                zShiftSpeed = Random.Range(minShiftSpeed, maxShiftSpeed);
            }
            if (lvlRotation && RotationObject)
                RotationObject.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
        }
    }

    private void NewGenerator(MeshGenerator2 generator)
    {
        // Noise generation
        generator.xNoise = Random.Range(minNoise, maxNoise);
        if (lvlNoiseSimilar)
            generator.zNoise = generator.xNoise + generator.xNoise * Random.Range(0.5f / lvlDifficulty, 1.0f / lvlDifficulty) * (Random.Range(0, 2) * 2 - 1);
        else
            generator.zNoise = Random.Range(minNoise, maxNoise);

        // Height generation
        generator.Noise = Random.Range(minHeight, maxHeight);

        // Shift generation
        generator.xNoiseShift += Random.Range(0.0f, 1000.0f);
        generator.zNoiseShift += Random.Range(0.0f, 1000.0f);
    }

    private void CopyGenerator(MeshGenerator2 orginal, MeshGenerator2 copy)
    {
        copy.xNoise = orginal.xNoise;
        copy.zNoise = orginal.zNoise;
        copy.xNoiseShift = orginal.xNoiseShift;
        copy.zNoiseShift = orginal.zNoiseShift;
        copy.Noise = orginal.Noise;
    }

    private void ModifyGeneartor(MeshGenerator2 generator, float similarity = 1.0f)
    {
        similarity = similarity < 1.0f ? 1.0f : similarity;

        generator.xNoise += generator.xNoise * Random.Range(0.5f / similarity, 1.0f / similarity) * (Random.Range(0, 2) * 2 - 1);
        generator.zNoise += generator.zNoise * Random.Range(0.5f / similarity, 1.0f / similarity) * (Random.Range(0, 2) * 2 - 1);
        generator.xNoiseShift += Random.Range(0.0f, 1000.0f);
        generator.zNoiseShift += Random.Range(0.0f, 1000.0f);

        if (lvlRotation)
        {
            float tmp = generator.xNoise;
            generator.xNoise = generator.zNoise;
            generator.zNoise = tmp;
        }
    }

    public void WrongButton()
    {
        if (!isActualCorrect)
        {
            Points += 1;
            NumberDisplay.SendMessage("UpdateCorrect", Points);
        }
        else
        {
            Errors += 1;
            NumberDisplay.SendMessage("UpdateErrors", Errors);
        }
        GenerateNewQuestion();
    }

    public void CorrectButton()
    {
        if (isActualCorrect)
        {
            Points += 1;
            NumberDisplay.SendMessage("UpdateCorrect", Points);
        }
        else
        {
            Errors += 1;
            NumberDisplay.SendMessage("UpdateErrors", Errors);
        }
        GenerateNewQuestion();
    }
}
