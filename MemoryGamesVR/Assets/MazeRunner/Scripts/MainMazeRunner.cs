using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMazeRunner : MonoBehaviour
{
    [SerializeField] private GameObject northDoor, eastDoor, southDoor, westDoor;
    [SerializeField] private GameObject northCanvas, eastCanvas, southCanvas, westCanvas;
    [SerializeField] private GameObject northWall, eastWall, southWall, westWall;
    [SerializeField] private GameObject carpetXD;
    [SerializeField] private GameObject key, obstacle, obstacle1, obstacle2;
    [SerializeField] private GameObject keyCanvas;
    [SerializeField] private GameObject mapCanvas;
    [SerializeField] private GameObject mapCanvas1;
    [SerializeField] private GameObject mapCanvas2;
    [SerializeField] private GameObject pointsCanvas;
    [SerializeField] private GameObject pointsText;
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject ladder;
    [SerializeField] private GameObject ladderCanvas;
    [SerializeField] private GameObject clockAsTimer;
    [SerializeField] private Sprite ladderSprite, skullSprite, keySprite, roomSprite, knightSprite;
    [SerializeField] private int difficulty = 1;
    [SerializeField] private GameObject[] mapFields;
    [SerializeField] private GameObject[] mapFields1;
    [SerializeField] private GameObject[] mapFields2;
    [SerializeField] private GameObject[] randomObjects;

    private int xPos = 0, yPos = 0;
    private int xSize, ySize;
    private int minRouteSize, maxRouteSize;
    private List<List<int>> mazeMatrix;

    private int keysToCollect = 3;
    private bool keyCollected = false;
    private int keysCollected = 0;
    private int keysCollectedOverall = 0;
    private int obstaclesEncountered = 0;
    private int obstaclesEncounteredOverall = 0;
    private bool obstacleEncountered = false;
    public int gamePhase = 0;
    private int levelsPassed = 0;
    private int points = 0;

    private float currTime = 0;
    private float maxTime = 30;

    // Start is called before the first frame update
    void Start()
    {
        gamePhase = 2;
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
        {
            this.difficulty = PlayerPrefs.GetInt("curr_game_difficulty");
        }

        northDoor.SetActive(true);
        northCanvas.SetActive(true);
        eastDoor.SetActive(true);
        eastCanvas.SetActive(true);
        southDoor.SetActive(false);
        southCanvas.SetActive(false);
        westDoor.SetActive(false);
        westCanvas.SetActive(false);
        northWall.SetActive(false);
        eastWall.SetActive(false);
        southWall.SetActive(true);
        westWall.SetActive(true);
        carpetXD.SetActive(true);
        pointsCanvas.SetActive(false);
        startCanvas.SetActive(true);
        clockAsTimer.SetActive(true);
        ladder.SetActive(false);
        ladderCanvas.SetActive(false);
        key.SetActive(false);
        keyCanvas.SetActive(false);
        obstacle.SetActive(false);
        obstacle1.SetActive(false);
        obstacle2.SetActive(false);

        randomObjectDecorations();

        this.levelsPassed = 0;

        if (this.difficulty < 5)
        {
            xSize = 5;
            ySize = 5;
        }
        else if (this.difficulty < 9)
        {
            xSize = 7;
            ySize = 7;
        }
        else
        {
            xSize = 9;
            ySize = 9;
        }

        if (xSize == 5)
        {
            mapCanvas.SetActive(true);
            mapCanvas1.SetActive(false);
            mapCanvas2.SetActive(false);
        }
        else if (xSize == 7)
        {
            mapCanvas.SetActive(false);
            mapCanvas1.SetActive(true);
            mapCanvas2.SetActive(false);
        }
        else
        {
            mapCanvas.SetActive(false);
            mapCanvas1.SetActive(false);
            mapCanvas2.SetActive(true);
        }

        keysToCollect = (int)(this.difficulty / 2) + 1;

        maxRouteSize = xSize + ySize + this.difficulty - 2;

        Debug.Log("xSize is: "); Debug.Log(xSize);
        Debug.Log("ySize is: "); Debug.Log(ySize);

        mazeMatrix = new List<List<int>>();

        for (int i = 0; i < xSize; i++)
        {
            List<int> oneRow = new List<int>();
            for (int j = 0; j < ySize; j++)
                oneRow.Add(0);
            mazeMatrix.Add(oneRow);
        }
        mazeMatrix[0][0] = 1;
        mazeMatrix[xSize - 1][ySize - 1] = 2;

        generateRandomRoute();

        generateRandomKeys();

        generateRandomObstacles();

        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                int fieldPosition = j + i * xSize;

                if (xSize == 5)
                {
                    mapFields[fieldPosition].SetActive(true);
                    switch (mazeMatrix[j][i])
                    {
                        case 1:
                            mapFields[fieldPosition].GetComponent<SpriteRenderer>().sprite = knightSprite;
                            break;
                        case 2:
                            mapFields[fieldPosition].GetComponent<SpriteRenderer>().sprite = ladderSprite;
                            break;
                        case 5:
                            mapFields[fieldPosition].GetComponent<SpriteRenderer>().sprite = skullSprite;
                            break;
                        case 6:
                            mapFields[fieldPosition].GetComponent<SpriteRenderer>().sprite = keySprite;
                            break;
                        default:
                            mapFields[fieldPosition].GetComponent<SpriteRenderer>().sprite = roomSprite;
                            break;
                    }
                }
                else if (xSize == 7)
                {
                    mapFields1[fieldPosition].SetActive(true);
                    switch (mazeMatrix[j][i])
                    {
                        case 1:
                            mapFields1[fieldPosition].GetComponent<SpriteRenderer>().sprite = knightSprite;
                            break;
                        case 2:
                            mapFields1[fieldPosition].GetComponent<SpriteRenderer>().sprite = ladderSprite;
                            break;
                        case 5:
                            mapFields1[fieldPosition].GetComponent<SpriteRenderer>().sprite = skullSprite;
                            break;
                        case 6:
                            mapFields1[fieldPosition].GetComponent<SpriteRenderer>().sprite = keySprite;
                            break;
                        default:
                            mapFields1[fieldPosition].GetComponent<SpriteRenderer>().sprite = roomSprite;
                            break;
                    }
                }
                else
                {
                    mapFields2[fieldPosition].SetActive(true);
                    switch (mazeMatrix[j][i])
                    {
                        case 1:
                            mapFields2[fieldPosition].GetComponent<SpriteRenderer>().sprite = knightSprite;
                            break;
                        case 2:
                            mapFields2[fieldPosition].GetComponent<SpriteRenderer>().sprite = ladderSprite;
                            break;
                        case 5:
                            mapFields2[fieldPosition].GetComponent<SpriteRenderer>().sprite = skullSprite;
                            break;
                        case 6:
                            mapFields2[fieldPosition].GetComponent<SpriteRenderer>().sprite = keySprite;
                            break;
                        default:
                            mapFields2[fieldPosition].GetComponent<SpriteRenderer>().sprite = roomSprite;
                            break;
                    }
                }
            }
        }

        string stringMap = "\n| ";
        for (int i = ySize - 1; i >= 0; i--)
        {
            for (int j = 0; j < xSize; j++)
            {
                stringMap += mazeMatrix[i][j].ToString();
                stringMap += " | ";
            }
            if (i != 0) stringMap += "\n| ";
        }
        stringMap += "\n";

        Debug.Log(stringMap);

        timersChangeMaxTime();
    }

    void BeginGame()
    {
        gamePhase = 0;

        northDoor.SetActive(true);
        northCanvas.SetActive(true);
        eastDoor.SetActive(true);
        eastCanvas.SetActive(true);
        southDoor.SetActive(false);
        southCanvas.SetActive(false);
        westDoor.SetActive(false);
        westCanvas.SetActive(false);
        northWall.SetActive(false);
        eastWall.SetActive(false);
        southWall.SetActive(true);
        westWall.SetActive(true);
        carpetXD.SetActive(true);
        pointsCanvas.SetActive(false);
        startCanvas.SetActive(true);
        clockAsTimer.SetActive(true);
        ladder.SetActive(false);
        ladderCanvas.SetActive(false);
        key.SetActive(false);
        keyCanvas.SetActive(false);
        obstacle.SetActive(false);
        obstacle1.SetActive(false);
        obstacle2.SetActive(false);

        if (this.levelsPassed > 0)
        {
            randomObjectDecorations();

            if (this.difficulty < 5)
            {
                xSize = 5;
                ySize = 5;
            }
            else if (this.difficulty < 9)
            {
                xSize = 7;
                ySize = 7;
            }
            else
            {
                xSize = 9;
                ySize = 9;
            }

            if (xSize == 5)
            {
                mapCanvas.SetActive(true);
                mapCanvas1.SetActive(false);
                mapCanvas2.SetActive(false);
            }
            else if (xSize == 7)
            {
                mapCanvas.SetActive(false);
                mapCanvas1.SetActive(true);
                mapCanvas2.SetActive(false);
            }
            else
            {
                mapCanvas.SetActive(false);
                mapCanvas1.SetActive(false);
                mapCanvas2.SetActive(true);
            }

            keysToCollect = (int)(this.difficulty / 2) + 1;

            maxRouteSize = xSize + ySize + this.difficulty - 2;

            Debug.Log("xSize is: "); Debug.Log(xSize);
            Debug.Log("ySize is: "); Debug.Log(ySize);

            mazeMatrix = new List<List<int>>();

            for (int i = 0; i < xSize; i++)
            {
                List<int> oneRow = new List<int>();
                for (int j = 0; j < ySize; j++)
                    oneRow.Add(0);
                mazeMatrix.Add(oneRow);
            }
            mazeMatrix[0][0] = 1;
            mazeMatrix[xSize - 1][ySize - 1] = 2;

            generateRandomRoute();

            generateRandomKeys();

            generateRandomObstacles();

            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    int fieldPosition = j + i * xSize;

                    if (xSize == 5)
                    {
                        mapFields[fieldPosition].SetActive(true);
                        switch (mazeMatrix[j][i])
                        {
                            case 1:
                                mapFields[fieldPosition].GetComponent<SpriteRenderer>().sprite = knightSprite;
                                break;
                            case 2:
                                mapFields[fieldPosition].GetComponent<SpriteRenderer>().sprite = ladderSprite;
                                break;
                            case 5:
                                mapFields[fieldPosition].GetComponent<SpriteRenderer>().sprite = skullSprite;
                                break;
                            case 6:
                                mapFields[fieldPosition].GetComponent<SpriteRenderer>().sprite = keySprite;
                                break;
                            default:
                                mapFields[fieldPosition].GetComponent<SpriteRenderer>().sprite = roomSprite;
                                break;
                        }
                    }
                    else if (xSize == 7)
                    {
                        mapFields1[fieldPosition].SetActive(true);
                        switch (mazeMatrix[j][i])
                        {
                            case 1:
                                mapFields1[fieldPosition].GetComponent<SpriteRenderer>().sprite = knightSprite;
                                break;
                            case 2:
                                mapFields1[fieldPosition].GetComponent<SpriteRenderer>().sprite = ladderSprite;
                                break;
                            case 5:
                                mapFields1[fieldPosition].GetComponent<SpriteRenderer>().sprite = skullSprite;
                                break;
                            case 6:
                                mapFields1[fieldPosition].GetComponent<SpriteRenderer>().sprite = keySprite;
                                break;
                            default:
                                mapFields1[fieldPosition].GetComponent<SpriteRenderer>().sprite = roomSprite;
                                break;
                        }
                    }
                    else
                    {
                        mapFields2[fieldPosition].SetActive(true);
                        switch (mazeMatrix[j][i])
                        {
                            case 1:
                                mapFields2[fieldPosition].GetComponent<SpriteRenderer>().sprite = knightSprite;
                                break;
                            case 2:
                                mapFields2[fieldPosition].GetComponent<SpriteRenderer>().sprite = ladderSprite;
                                break;
                            case 5:
                                mapFields2[fieldPosition].GetComponent<SpriteRenderer>().sprite = skullSprite;
                                break;
                            case 6:
                                mapFields2[fieldPosition].GetComponent<SpriteRenderer>().sprite = keySprite;
                                break;
                            default:
                                mapFields2[fieldPosition].GetComponent<SpriteRenderer>().sprite = roomSprite;
                                break;
                        }
                    }
                }
            }

            string stringMap = "\n| ";
            for (int i = ySize - 1; i >= 0; i--)
            {
                for (int j = 0; j < xSize; j++)
                {
                    stringMap += mazeMatrix[i][j].ToString();
                    stringMap += " | ";
                }
                if (i != 0) stringMap += "\n| ";
            }
            stringMap += "\n";

            Debug.Log(stringMap);

            timersChangeMaxTime();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= maxTime && gamePhase == 0)
        {
            calculatePoints();
        }

        if (gamePhase == 2)
        {
            currTime = 0;
            startCanvas.SetActive(true);
        }
        else
        {
            startCanvas.SetActive(false);
        }

        timersChangeCurrTime();

        if (xPos == 0)
        {
            westDoor.SetActive(false);
            westCanvas.SetActive(false);
            westWall.SetActive(true);
        }
        else
        {
            westDoor.SetActive(true);
            westCanvas.SetActive(true);
            westWall.SetActive(false);
        }

        if (yPos == 0)
        {
            southDoor.SetActive(false);
            southCanvas.SetActive(false);
            southWall.SetActive(true);
        }
        else
        {
            southDoor.SetActive(true);
            southCanvas.SetActive(true);
            southWall.SetActive(false);
        }

        if (xPos == xSize - 1)
        {
            eastDoor.SetActive(false);
            eastCanvas.SetActive(false);
            eastWall.SetActive(true);
        }
        else
        {
            eastDoor.SetActive(true);
            eastCanvas.SetActive(true);
            eastWall.SetActive(false);
        }

        if (yPos == ySize - 1)
        {
            northDoor.SetActive(false);
            northCanvas.SetActive(false);
            northWall.SetActive(true);
        }
        else
        {
            northDoor.SetActive(true);
            northCanvas.SetActive(true);
            northWall.SetActive(false);
        }

        if (xPos == 0 && yPos == 0)
        {
            if (xSize == 5)
            {
                mapCanvas.SetActive(true);
                mapCanvas1.SetActive(false);
                mapCanvas2.SetActive(false);
            }
            else if (xSize == 7)
            {
                mapCanvas.SetActive(false);
                mapCanvas1.SetActive(true);
                mapCanvas2.SetActive(false);
            }
            else
            {
                mapCanvas.SetActive(false);
                mapCanvas1.SetActive(false);
                mapCanvas2.SetActive(true);
            }
        }
        else
        {
            mapCanvas.SetActive(false);
            mapCanvas1.SetActive(false);
            mapCanvas2.SetActive(false);
        }

        if ((xPos == xSize - 1) && (yPos == ySize - 1) && (keysCollected == keysToCollect))
        {
            ladder.SetActive(true);
            ladderCanvas.SetActive(true);
        }
        else
        {
            ladder.SetActive(false);
            ladderCanvas.SetActive(false);
        }

        if (mazeMatrix[xPos][yPos] == 6)
        {
            if (keyCollected == true)
            {
                key.SetActive(false);
                keyCanvas.SetActive(false);
            }
            else if (keysCollected < keysToCollect)
            {
                key.SetActive(true);
                keyCanvas.SetActive(true);
            }
        }
        else
        {
            key.SetActive(false);
            keyCanvas.SetActive(false);
        }

        if (mazeMatrix[xPos][yPos] == 5)
        {
            if (obstacleEncountered == false)
            {
                obstaclesEncountered++;
                obstacleEncountered = true;
            }
            obstacle.SetActive(true);
            obstacle1.SetActive(true);
            obstacle2.SetActive(true);
        }
        else
        {
            obstacle.SetActive(false);
            obstacle1.SetActive(false);
            obstacle2.SetActive(false);
        }
    }

    public void StartGameButton()
    {
        BeginGame();
    }

    public void UpdatePosition(int doorId)
    {
        switch (doorId)
        {
            case 1:
                Debug.Log("doorId is: "); Debug.Log(doorId);
                if (yPos < ySize - 1) yPos++;
                break;
            case 2:
                Debug.Log("doorId is: "); Debug.Log(doorId);
                if (xPos < xSize - 1) xPos++;
                break;
            case 3:
                Debug.Log("doorId is: "); Debug.Log(doorId);
                if (yPos > 0) yPos--;
                break;
            case 4:
                Debug.Log("doorId is: "); Debug.Log(doorId);
                if (xPos > 0) xPos--;
                break;
            default:
                break;
        }

        keyCollected = false;
        obstacleEncountered = false;

        randomObjectDecorations();
    }

    private void randomObjectDecorations()
    {
        int num;
        for (int i = 0; i < randomObjects.Length; i++)
        {
            num = Random.Range(0, 100);
            if (num <= 50)
                randomObjects[i].SetActive(true);
            else
                randomObjects[i].SetActive(false);
        }
    }

    private void generateRandomRoute()
    {
        int currX = 0;
        int currY = 0;
        int endX = xSize - 1;
        int endY = ySize - 1;

        bool pathFound = false;

        List<Position> foundPath = new List<Position>();

        foundPath.Add(new Position(currX, currY));

        while (!pathFound)
        {
            if (endX > currX && endY > currY)
            {
                int rand = Random.Range(0, 100);
                if (rand < 50) currX++;
                else currY++;
            }
            else if (endX > currX)
            {
                currX++;
            }
            else if (endY > currY)
            {
                currY++;
            }

            if (currX == endX && currY == endY)
            {
                pathFound = true;
                continue;
            }

            foundPath.Add(new Position(currX, currY));
            maxRouteSize--;
        }

        for (int i = 1; i < foundPath.Count; i++)
        {
            mazeMatrix[foundPath[i].getPosX()][foundPath[i].getPosY()] = 3;
        }
    }

    private void generateRandomKeys()
    {
        int keysToPut = keysToCollect;

        while (keysToPut > 0)
        {
            int randX = Random.Range(0, xSize - 1);
            int randY = Random.Range(0, ySize - 1);

            if (mazeMatrix[randX][randY] != 3) continue;

            mazeMatrix[randX][randY] = 6;
            keysToPut--;
        }
    }

    public void updateKeysCount()
    {
        if (keyCollected == false)
        {
            keysCollected++;
            keyCollected = true;
            mazeMatrix[xPos][yPos] = 17;
        }

        Debug.Log(keysCollected);
    }

    private void generateRandomObstacles()
    {
        int obstaclesToPut = (this.difficulty * 2) - 1;

        while (obstaclesToPut > 0)
        {
            int randX = Random.Range(0, xSize - 1);
            int randY = Random.Range(0, ySize - 1);

            if (mazeMatrix[randX][randY] != 0) continue;

            mazeMatrix[randX][randY] = 5;
            obstaclesToPut--;
        }
    }

    public void calculatePoints()
    {
        gamePhase = 1;
        mapCanvas.SetActive(false);
        mapCanvas1.SetActive(false);
        mapCanvas2.SetActive(false);

        //Debug.Log("Konczymy");

        //this.points = 500;

        for (int i = 0; i < keysCollected + keysCollectedOverall; i++)
            points += 93;

        for (int i = 0; i < obstaclesEncountered + obstaclesEncounteredOverall; i++)
            points -= 257;

        //float distance = Mathf.Sqrt(Mathf.Pow(xPos - xSize + 1, 2) + Mathf.Pow(yPos - ySize + 1, 2));

        //Debug.Log(distance);

        points += 269 * levelsPassed;
        points = points / 2000;

        //points += (int)(72 / distance);

        if (points < 0)
            points = 0;
        else if (points > 100)
            points = 100;

        string pointsString = "";
        pointsString = points.ToString();
        pointsString += "%";

        Debug.Log("punkty" + points);

        pointsText.GetComponent<TMPro.TextMeshProUGUI>().text = pointsString;

        pointsCanvas.SetActive(true);        
    }

    public void EndGameButton()
    {
        GameChoiceManager game_manager = GameObject.FindObjectOfType<GameChoiceManager>();
        game_manager.endGameManagement(points);
    }

    public void nextLevel()
    {
        this.levelsPassed += 1;

        if (this.difficulty == 10)
            calculatePoints();

        keysCollectedOverall += keysCollected;
        obstaclesEncounteredOverall += obstaclesEncountered;

        keysCollected = 0;
        obstaclesEncountered = 0;
        keyCollected = false;
        obstacleEncountered = false;

        xPos = 0;
        yPos = 0;
        difficulty += 1;

        BeginGame();
    }

    public void ladderClicked()
    {
        nextLevel();
    }

    public void timersChangeMaxTime()
    {
        ClockAsTimer[] timers_list = Object.FindObjectsOfType<ClockAsTimer>();
        for (int i = 0; i < timers_list.Length; i++)
        {
            timers_list[i].setMaxTime(maxTime);
        }
    }

    public void timersChangeCurrTime()
    {
        ClockAsTimer[] timers_list = Object.FindObjectsOfType<ClockAsTimer>();
        for (int i = 0; i < timers_list.Length; i++)
        {
            timers_list[i].setCurrTime(currTime);
        }
    }

    private class Position
    {
        public int xPos;
        public int yPos;

        public Position(int x, int y)
        {
            this.xPos = x;
            this.yPos = y;
        }

        public void setPos(int x, int y)
        {
            this.xPos = x;
            this.yPos = y;
        }

        public int getPosX()
        {
            return this.xPos;
        }

        public int getPosY()
        {
            return this.yPos;
        }
    }
}
