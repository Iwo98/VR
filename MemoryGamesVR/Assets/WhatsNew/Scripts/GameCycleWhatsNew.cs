using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCycleWhatsNew : MonoBehaviour
{

    List<int> invisibleItems = new List<int>();
    [HideInInspector] public List<ClickWN> itemsInGame = new List<ClickWN>();
    [HideInInspector] public bool areNewItemsShown = false;
    [HideInInspector] public int gameState = 0;
    [HideInInspector] public int score = 0;
    [HideInInspector] private int addedItemsCount = 0;

    public float timeToFinishGame = 22.0f;

    [SerializeField]
    public float timeToSpawnItems = 8.0f;
    public int gameLevel = 7;
    public GameObject canvasScore;
    public GameObject rememberTimeBars;
    public GameObject playTimeBars;
    public GameObject endCanvas;
    public GameObject rememberTimeBar1;
    public GameObject rememberTimeBar2;
    public GameObject rememberTimeBar3;
    public GameObject rememberTimeBar4;
    public GameObject playTimeBar1;
    public GameObject playTimeBar2;
    public GameObject playTimeBar3;
    public GameObject playTimeBar4;
    public TMP_Text scoreText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
        {
            gameLevel = PlayerPrefs.GetInt("curr_game_difficulty");
        }

        gameState = 0;
        if (gameLevel > 10)
            gameLevel = 10;
        if (gameLevel < 1)
            gameLevel = 1;
    }
    // Start is called before the first frame update
    void Start()
    {

        ChoseItemsForGame(gameLevel);
        PlaceItemsOnShelves(gameLevel);
        ChooseInvisibleItems(gameLevel);
        SetItemsInvisible();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case 0:
                break;
            case 1:
                GenerateItems();
                gameState += 1;
                Debug.Log("Aktywacja paska zapamiętywania");
                rememberTimeBars.SetActive(true);
                AnimateRememberBar();
                break;
            case 2:
                CountPreswapnTime();
                if (timeToSpawnItems < 0)
                {
                    gameState += 1;
                    Debug.Log("Aktywacja paska gry");
                    rememberTimeBars.SetActive(false);
                    playTimeBars.SetActive(true);
                    AnimatePlayBar();
                }
                break;
            case 3:
                CountGameTime();
                break;
            case 4:
                break;
            default:
                break;

        }

    }

    void GenerateItems()
    {

    }

    void ChooseInvisibleItems(int gameLevel)
    {
        int amountOfItems = GetComponent<ClickWN>().clickOns.Count;

        //draw items that are to be invisible 
        for (int i = 0; i < amountOfItems; i++)
        {
            if ((Random.value > 0.5) && !(invisibleItems.Count >= amountOfItems / 2))
                invisibleItems.Add(i);
        }

        addedItemsCount = invisibleItems.Count;
    }

    public void SetItemsInvisible()
    {
        foreach (var idx in invisibleItems)
        {
            GetComponent<ClickWN>().clickOns[idx].GetComponent<MeshRenderer>().enabled = false;
            GetComponent<ClickWN>().clickOns[idx].wasInvisible = true;
        }
    }

    public void SetItemsVisible()
    {
        foreach (var idx in invisibleItems)
        {
            GetComponent<ClickWN>().clickOns[idx].GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void StartGame()
    {

        gameState = 1;

    }

    void CountPreswapnTime()
    {
        timeToSpawnItems -= Time.deltaTime;
        if (timeToSpawnItems < 0)
        {
            this.SetItemsVisible();
            areNewItemsShown = true;
        }

    }

    void CountGameTime()
    {
        timeToFinishGame -= Time.deltaTime;
        if (timeToFinishGame < 0) 
        {
            this.EndGame();
        }
    }

    void EndGame()
    {
        gameState++;
        playTimeBars.SetActive(false);
        endCanvas.SetActive(true);
        canvasScore.SetActive(true);
        if (score < 0)
            score = 0;
        scoreText.text = Mathf.RoundToInt(100 * score / addedItemsCount).ToString() + "%";
        //score = (int)((1300 * score / addedItemsCount) * (1 + 1.3 * (gameLevel - 1)));
        score = (int)Mathf.RoundToInt(100 * score / addedItemsCount);

    }

    void ChoseItemsForGame(int gameLevel)
    {
        this.GetComponent<ClickWN>().ChoseItemsForGame(gameLevel);
    }

    void PlaceItemsOnShelves(int gameLevel)
    {
        this.GetComponent<ClickWN>().PlaceItemsOnShelves(gameLevel);
    }

    private void AnimateRememberBar()
    {   
        LeanTween.scaleX(rememberTimeBar1, 0, timeToSpawnItems);
        LeanTween.scaleX(rememberTimeBar2, 0, timeToSpawnItems);
        LeanTween.scaleX(rememberTimeBar3, 0, timeToSpawnItems);
        LeanTween.scaleX(rememberTimeBar4, 0, timeToSpawnItems);
    }

    private void AnimatePlayBar()
    {
        LeanTween.scaleX(playTimeBar1, 0, timeToFinishGame);
        LeanTween.scaleX(playTimeBar2, 0, timeToFinishGame);
        LeanTween.scaleX(playTimeBar3, 0, timeToFinishGame);
        LeanTween.scaleX(playTimeBar4, 0, timeToFinishGame);
    }
}
