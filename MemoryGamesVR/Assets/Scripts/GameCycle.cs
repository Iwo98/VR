using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour
{

    List<int> invisibleItems = new List<int>();
    [HideInInspector] public List<Click> itemsInGame = new List<Click>();
    [HideInInspector] public bool areNewItemsShown = false;
    [HideInInspector] public int gameState = 1;

    [SerializeField]
    public float timeToSpawnItems = 5.0f;
    public int gameLevel = 1;

    private void Awake()
    {
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
            case 1:
                GenerateItems();
                gameState += 1;
                break;
            case 2:
                CountTime();
                if (timeToSpawnItems < 0)
                    gameState += 1;
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
        int amountOfItems = GetComponent<Click>().clickOns.Count;

        //draw items that are to be invisible 
        for (int i = 0; i < amountOfItems; i++)
        {
            if ((Random.value > 0.5) && !(invisibleItems.Count >= amountOfItems / 2))
                invisibleItems.Add(i);
        }
    }

    public void SetItemsInvisible()
    {
        foreach (var idx in invisibleItems)
        {
            //GetComponent<Click>().clickOns[idx].GetComponent(MeshRenderer).enabled = false;
            GetComponent<Click>().clickOns[idx].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void SetItemsVisible()
    {
        foreach (var idx in invisibleItems)
        {
            //GetComponent<Click>().clickOns[idx].GetComponent(MeshRenderer).enabled = false;
            GetComponent<Click>().clickOns[idx].GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void CountTime()
    {
        timeToSpawnItems -= Time.deltaTime;
        if (timeToSpawnItems < 0)
        {
            GetComponent<GameCycle>().SetItemsVisible();
            areNewItemsShown = true;
        }
    }

    void ChoseItemsForGame(int gameLevel)
    {
        this.GetComponent<Click>().ChoseItemsForGame(gameLevel);
    }

    void PlaceItemsOnShelves(int gameLevel)
    {
        this.GetComponent<Click>().PlaceItemsOnShelves(gameLevel);
    }
}
