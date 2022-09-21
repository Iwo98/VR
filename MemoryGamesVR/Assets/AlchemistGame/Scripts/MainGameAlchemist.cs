using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MainGameAlchemist : MonoBehaviour
{
    public int difficulty;
    public int rememberPhaseTime;
    public int actionPhaseTime;

    private int maxDifficulty = 10;
    private int potionsOnShelf = 3;
    private float potionsXChange = 0.45f;
    private float potionsYChange = 0.37f;
    private float potionsXRandom = 0.15f;
    private Vector3 potionsStartPos = new Vector3(0, 0, 0);
    private Quaternion potionStartRot = new Quaternion(0, 90, 90, 0);

    private List<int> potionIds;
    private int numberOfPotions;
    private int numberOfColPotions;
    private int numberOfColors;
    private int numberOfItemPotions;

    private List<int> recipe;
    private List<int> recipeCorrect;
    private int recipeId = 0;
    private int recipeLen = 3;

    private int score = 0;
    private int potionScore = 55;
    private int recipeScore = 20;
    private float difficulty_score_mul = 0.3f;
    private int correctPotions = 0;
    private int wrongPotions = 0;
    private float scoreToGoldMultiplier = 2.755f;

    public GameObject potionTemplate1;
    public GameObject potionTemplate2;
    public List<Material> PotionColors;
    public Material PotionColNeutral;
    public List<GameObject> itemPotionTemplates;

    private int currGamePhase = 3;
    private float currTime = 0;
    private float maxTime = 60;
    public GameObject clockTimer;

    public GameObject recipeScroll;
    public GameObject explosionTemplate;
    public GameObject potionStartPosition, explosionPoint;
    public GameObject startMenuCanvas, endMenuCanvas, pointsCanvas;
    public GameObject textScore, textGold, textSilver, textBronze;

    public AudioSource audioSource;
    public AudioClip glassBreak;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
        {
            difficulty = PlayerPrefs.GetInt("curr_game_difficulty");
        }

        currGamePhase = -1;
        changeGamePhase();
    }


    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if(currGamePhase == 1 || currGamePhase == 2)
        {
            timersChangeCurrTime();
            if (currTime > maxTime)
            {
                changeGamePhase();
            }
        }
        else if(currGamePhase == 3)
        {
            int score_temp = (int)(score * scoreToGoldMultiplier);
            if(currTime < 5)
            {
                score_temp = (int)(score_temp * (currTime / 5.0f));
            }

            textGold.GetComponent<TextMeshProUGUI>().text = (score_temp / 100).ToString();
            score_temp -= 100 * (score_temp / 100);
            textSilver.GetComponent<TextMeshProUGUI>().text = (score_temp / 10).ToString();
            score_temp -= 10 * (score_temp / 10);
            textBronze.GetComponent<TextMeshProUGUI>().text = score_temp.ToString();
        }
    }

    private void restartGame()
    {
        score = 0;
        correctPotions = 0;
        wrongPotions = 0;
        initializeGameValues();
        destroyPotions();
    }

    private void initializeGameValues()
    {
        if (rememberPhaseTime < 1)
        {
            rememberPhaseTime = 1;
        }
        if (actionPhaseTime < 1)
        {
            actionPhaseTime = 1;
        }

        if (difficulty > maxDifficulty)
        {
            difficulty = maxDifficulty;
        }
        else if (difficulty < 1)
        {
            difficulty = 1;
        }

        switch (difficulty)
        {
            case 1:
                numberOfColPotions  = 3;
                numberOfColors      = 1;
                numberOfItemPotions = 0;
                recipeLen           = 3;
                break;
            case 2:
                numberOfColPotions  = 4;
                numberOfColors      = 1;
                numberOfItemPotions = 0;
                recipeLen           = 3;
                break;
            case 3:
                numberOfColPotions  = 5;
                numberOfColors      = 1;
                numberOfItemPotions = 0;
                recipeLen           = 4;
                break;
            case 4:
                numberOfColPotions  = 3;
                numberOfColors      = 1;
                numberOfItemPotions = 2;
                recipeLen           = 4;
                break;
            case 5:
                numberOfColPotions  = 4;
                numberOfColors      = 1;
                numberOfItemPotions = 2;
                recipeLen           = 4;
                break;
            case 6:
                numberOfColPotions  = 5;
                numberOfColors      = 1;
                numberOfItemPotions = 2;
                recipeLen           = 5;
                break;
            case 7:
                numberOfColPotions  = 5;
                numberOfColors      = 2;
                numberOfItemPotions = 2;
                recipeLen           = 5;
                break;
            case 8:
                numberOfColPotions  = 6;
                numberOfColors      = 2;
                numberOfItemPotions = 2;
                recipeLen           = 5;
                break;
            case 9:
                numberOfColPotions  = 5;
                numberOfColors      = 2;
                numberOfItemPotions = 3;
                recipeLen           = 5;
                break;
            default:  // Difficulty 10
                numberOfColPotions  = 6;
                numberOfColors      = 2;
                numberOfItemPotions = 3;
                recipeLen           = 5;
                break;
        }

        numberOfPotions = numberOfColPotions + numberOfItemPotions;

    }

    private void destroyPotions()
    {
        Potion[] old_potions = GameObject.FindObjectsOfType<Potion>();
        for (int i = 0; i < old_potions.Length; i++)
        {
            old_potions[i].destroyPotion();
        }

    }

    private void initializePotions()
    {
        potionsStartPos = potionStartPosition.transform.position;

        List<int> colorIds = new List<int>();
        for (int i = 0; i < PotionColors.Count; i++)
        {
            colorIds.Add(i);
        }
        Shuffle(colorIds);

        List<int> itemIds = new List<int>();
        for (int i = 0; i < itemPotionTemplates.Count; i++)
        {
            itemIds.Add(i + 20);
        }
        Shuffle(itemIds);

        potionIds = new List<int>();
        for (int i = 0; i < numberOfColPotions; i++)
        {
            if(numberOfColors == 2)
            {
                if (i % 2 == 0)
                    potionIds.Add(colorIds[(int)(i / 2)]);
                else
                    potionIds.Add(colorIds[(int)(i / 2)] + 10);
            }
            else
            {
                potionIds.Add(colorIds[i]);
            }
        }

        for (int i = 0; i < numberOfItemPotions; i++)
        {
            potionIds.Add(itemIds[i]);
        }
        Shuffle(potionIds);

        List<int> shelf_ids = new List<int>();
        int max_shelves = 1;
        if (numberOfPotions >= 8)
        {
            max_shelves = 4;
        }
        else if (numberOfPotions >= 6)
        {
            max_shelves = 3;
        }
        else if (numberOfPotions >= 4)
        {
            max_shelves = 2;
        }
        for (int i = 0; i < max_shelves * potionsOnShelf; i++)
        {
            shelf_ids.Add(i);
        }
        Shuffle(shelf_ids);

        for (int i = 0; i < potionIds.Count; i++)
        {
            Vector3 potionPos = potionsStartPos;
            potionPos.y += (shelf_ids[i] / potionsOnShelf) * potionsYChange;
            potionPos.x += (shelf_ids[i] % potionsOnShelf) * potionsXChange;
            if (difficulty >= 3)
            {
                potionPos.x += Random.Range(-1 * potionsXRandom * 0.5f, potionsXRandom * 0.5f);
            }
            else if (difficulty >= 5)
            {
                potionPos.x += Random.Range(-1 * potionsXRandom, potionsXRandom);
            }
            if (potionIds[i] < 10)
            {
                var pot_temp = Object.Instantiate(potionTemplate1, potionPos, potionStartRot);
                pot_temp.GetComponent<Renderer>().material = PotionColors[potionIds[i] % 10];
                pot_temp.GetComponent<Potion>().setPotionId(potionIds[i]);
            }
            else if (potionIds[i] < 20)
            {
                var pot_temp = Object.Instantiate(potionTemplate2, potionPos, potionStartRot);
                pot_temp.GetComponent<Renderer>().material = PotionColors[potionIds[i] % 10];
                pot_temp.GetComponent<Potion>().setPotionId(potionIds[i]);
            }
            else
            {
                var pot_temp = Object.Instantiate(itemPotionTemplates[potionIds[i] % 10], potionPos, potionStartRot);
                pot_temp.GetComponent<Potion>().setPotionId(potionIds[i]);
            }
        }
    }

    public void buttonClick(int id)
    {
        if (id == 0)
        {
            changeGamePhase();
        }
        else if (id == 1)
        {
            GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
            game_manager.endGameManagement(score);
        }
    }

    private void changeGamePhase()
    {
        currTime = 0;
        currGamePhase += 1;

        startMenuCanvas.GetComponent<Canvas>().enabled = false;
        endMenuCanvas.GetComponent<Canvas>().enabled = false;

        if (currGamePhase > 3)
        {
            currGamePhase = 0;
        }

        // Actions required at the start of each phase
        if (currGamePhase == 0) // Start
        {
            maxTime = 5;
            restartGame();
            recipeScroll.GetComponent<Transform>().Find("Canvas").GetComponent<Canvas>().enabled = false;
            startMenuCanvas.GetComponent<Canvas>().enabled = true;
            recipeScroll.GetComponent<RecipeScroll>().disableCanvas();
        }
        else if (currGamePhase == 1) // Remembering
        {
            initializePotions();
            maxTime = rememberPhaseTime;
        }
        else if(currGamePhase == 2) // Guessing
        {
            GameObject explosion = Object.Instantiate(explosionTemplate, explosionPoint.GetComponent<Transform>().position, Quaternion.identity);
            explosion.GetComponent<destroyEffect>().setAutomaticDestroy(4.0f);
            recipeScroll.GetComponent<Transform>().Find("Canvas").GetComponent<Canvas>().enabled = true;
            transformPotions();
            randomRecipe();
            maxTime = actionPhaseTime;
        }
        else if(currGamePhase == 3) // Game end
        {
            maxTime = 5;
            if (correctPotions + wrongPotions > 0)
            {
                score = (int)(score * correctPotions / (correctPotions + wrongPotions));
            }
            score = (int)(score * (1.0f + difficulty_score_mul * (difficulty - 1)));
            score = Mathf.RoundToInt(score / (700 * (1 + difficulty_score_mul * (difficulty - 1))));
            if (score > 100)
            {
                score = 100;
            }
            endMenuCanvas.GetComponent<Canvas>().enabled = true;
            textScore.GetComponent<TextMeshProUGUI>().text =  score.ToString();
        }
        timersChangeCurrTime();
        timersChangeMaxTime();
    }

    private void randomRecipe()
    {
        recipe = new List<int>();
        recipeCorrect = new List<int>();
        recipeId = 0;

        for (int i = 0; i < recipeLen; i++)
        {
            int id = Random.Range(0, potionIds.Count);
            if(i > 0)
            {
                while(potionIds[id] == recipe[i - 1])
                {
                    id = Random.Range(0, potionIds.Count);
                }
            }
            recipe.Add(potionIds[id]);
            recipeCorrect.Add(0);
        }
        recipeScroll.GetComponent<RecipeScroll>().updateRecipeValues(recipe, recipeCorrect, recipeId, recipeLen);
    }

    public void checkRecipe(int potionId)
    {
        if (currGamePhase == 2)
        {
            if (potionId == recipe[recipeId])
            {
                recipeCorrect[recipeId] = 1;
                score += (int)(potionScore * (1 + difficulty / maxDifficulty));
                correctPotions++;
            }
            else
            {
                recipeCorrect[recipeId] = -1;
                wrongPotions++;
            }
            recipeId += 1;
            recipeScroll.GetComponent<RecipeScroll>().updateRecipeValues(recipe, recipeCorrect, recipeId, recipeLen);

            if (recipeId >= recipeLen)
            {
                bool full_recipe_correct = true;
                int recipeCorrectPots = 0;
                for (int i = 0; i < recipeLen; i++)
                {
                    if (recipeCorrect[i] == -1)
                    {
                        full_recipe_correct = false;
                    }
                    else 
                    {
                        recipeCorrectPots += 1;
                    }
                }
                if (full_recipe_correct)
                {
                    score += (int)(recipeScore * recipeLen * (1 + difficulty / maxDifficulty));
                }

                Color textColor = new Color(1.0f, 0.5f, 0.0f);
                if (recipeCorrectPots == recipeLen)
                {
                    textColor = new Color(0.0f, 1.0f, 0.0f);
                }
                else if (recipeCorrectPots >= recipeLen / 2 + recipeLen % 2)
                {
                    textColor = new Color(1.0f, 1.0f, 0.0f);
                }
                else if (recipeCorrectPots == 0)
                {
                    textColor = new Color(1.0f, 0.0f, 0.0f);
                }

                pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().color = textColor;
                pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().text = recipeCorrectPots.ToString() + "/" + recipeLen.ToString();
                pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1.0f, 0.0f, false);
                pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0.0f, 2.5f, false);

                float waitTime = 0.75f;
                recipeScroll.GetComponent<RecipeScroll>().crossFadeCanvas(waitTime);
                StartCoroutine(fullRecipeWaiter(waitTime));
            }
        }
    }

    IEnumerator fullRecipeWaiter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        randomRecipe();
    }

    public void timersChangeMaxTime()
    {
        ClockTimer[] timers_list = Object.FindObjectsOfType<ClockTimer>();
        for(int i = 0; i < timers_list.Length; i++)
        {
            timers_list[i].setMaxTime(maxTime);
        }
    }

    public void timersChangeCurrTime()
    {
        ClockTimer[] timers_list = Object.FindObjectsOfType<ClockTimer>();
        for (int i = 0; i < timers_list.Length; i++)
        {
            timers_list[i].setCurrTime(currTime);
        }
    }

    public void transformPotions()
    {
        audioSource.clip = glassBreak;
        audioSource.volume = 0.3f;
        audioSource.Play();
        Potion[] potion_list = Object.FindObjectsOfType<Potion>();
        for (int i = 0; i < potion_list.Length; i++)
        {
            potion_list[i].transformPotion();
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
