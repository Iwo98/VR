using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainGameDragonFlight : MonoBehaviour
{
    public int difficulty;
    public int actionPhaseTime;
    
    private int magicGridSize = 2;
    private float magicGridDist = 0.0f;
    Vector3 gridStart = new Vector3(0, 0, 0);

    private List<int> possibleEdges = new List<int>();

    private List<Vector2Int> magicSignTemplate = new List<Vector2Int>();
    private int magicSignLengthMin = 3;
    private int magicSignLengthVar = 1;

    private bool isDrawing = false; 
    private int currentSphereId = -1;
    private List<Vector2Int> magicSignEgdes = new List<Vector2Int>();

    private int score = 0;
    private int spellEdgeScore = 40;
    private float scoreToDmgMultiplier = 5.775f;
    private List<int> spellScores = new List<int>();
    private int currSpellId = 0;

    private int currGamePhase = 3;
    private float currTime = 0;
    private float maxTime = 60;

    private float spellSpeed = 0.9f;
    private float spellTimer = 0.0f;
    private float spellLifetime = 1.5f;
    private List<GameObject> activeSpells;

    public GameObject magicSphereTemplate, sphereConnectorTemplate;

    public List<GameObject> spellObjects;
    public List<GameObject> explosionObjects;
    public GameObject playerCastingPoint, enemyMageCastingPoint;

    public GameObject startMenuCanvas, endMenuCanvas, timerCanvas, pointsCanvas, dmgCanvas;
    public GameObject btnStart, btnContinue;
    public GameObject textScore, textDamage;
    

    // Start is called before the first frame update
    void Start()
    {
        currGamePhase = -1;
        magicGridDist = 2.5f * magicSphereTemplate.GetComponent<MeshFilter>().transform.localScale.x;
        initializeButtonListeners();
        changeGamePhase();
    }


    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if(currGamePhase == 1)
        {
            if(isDrawing && !Input.GetMouseButton(0))
            {
                if(magicSignEgdes.Count > 0)
                {
                    isDrawing = false;
                    checkMagicSignTemplate();
                    randomMagicSignTemplate();
                }
                else
                {
                    isDrawing = false;
                    currentSphereId = -1;
                    redrawActiveSpheres();
                    drawMagicSignTemplate();
                }  
            }

            timersChangeCurrTime();
            if(currTime > actionPhaseTime)
            {
                isDrawing = false;
                changeGamePhase();
            }
        }
        else if(currGamePhase == 2)
        {
            int score_temp = (int)(score * scoreToDmgMultiplier);
            if (currTime < 5)
            {
                score_temp = (int)(score_temp * (currTime / 5.0f));
            }

            textDamage.GetComponent<TextMeshProUGUI>().text = score_temp.ToString();
        }

        if(spellTimer > 0.0f)
        {
            spellTimer -= Time.deltaTime;
            if(spellTimer <= 0.0f)
            {
                spellTimer = 0.0f;
                Vector3 explPos = activeSpells[0].GetComponent<Transform>().position;
                for (int i = 0; i < activeSpells.Count; i++)
                {
                    activeSpells[i].GetComponent<destroyEffect>().destroy();
                }

                int explId = Random.Range(0, explosionObjects.Count);
                GameObject explosion = Object.Instantiate(explosionObjects[explId], explPos, Quaternion.identity);
                explosion.GetComponent<destroyEffect>().setAutomaticDestroy(4.0f);

                dmgCanvas.GetComponent<Transform>().Find("dmgText").GetComponent<TextMeshProUGUI>().text = ((int)(spellScores[currSpellId] * scoreToDmgMultiplier)).ToString();
                dmgCanvas.GetComponent<Transform>().Find("dmgText").GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1.0f, 0.0f, false);
                dmgCanvas.GetComponent<Transform>().Find("dmgText").GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0.0f, 2.5f, false);
                dmgCanvas.GetComponent<Transform>().Find("dmgImg").GetComponent<Image>().enabled = true;
                dmgCanvas.GetComponent<Transform>().Find("dmgImg").GetComponent<Image>().CrossFadeAlpha(1.0f, 0.0f, false);
                dmgCanvas.GetComponent<Transform>().Find("dmgImg").GetComponent<Image>().CrossFadeAlpha(0.0f, 2.5f, false);

                currSpellId += 1;
            }
        }
    }

    private void restartGame()
    {
        dmgCanvas.GetComponent<Transform>().Find("dmgImg").GetComponent<Image>().enabled = false;
        initializeGameValues();
    }

    private void initializeGameValues()
    {
        currentSphereId = -1;
        magicSignTemplate = new List<Vector2Int>();
        isDrawing = false;
        magicSignEgdes = new List<Vector2Int>();
        score = 0;
    }

    private void initializeMagicSpheres()
    {
        MagicSphere[] oldSpheres = GameObject.FindObjectsOfType<MagicSphere>();
        for (int i = 0; i < oldSpheres.Length; i++)
        {
            Destroy(oldSpheres[i]);
        }

        gridStart = GameObject.Find("MagicSpheresStart").transform.position;

        for (int z_id = 0; z_id < magicGridSize; z_id++)
        {
            for (int y_id = 0; y_id < magicGridSize; y_id++)
            {
                for (int x_id = 0; x_id < magicGridSize; x_id++)
                {
                    Vector3 spherePos = gridStart;
                    spherePos.x += x_id * magicGridDist;
                    spherePos.y += y_id * magicGridDist;
                    spherePos.z += z_id * magicGridDist;

                    var sphere_temp = Object.Instantiate(magicSphereTemplate, spherePos, Quaternion.identity);
                    Vector3Int spherePoint = new Vector3Int(x_id, y_id, z_id);
                    int sphereId = z_id * magicGridSize * magicGridSize + y_id * magicGridSize + x_id;
                    possibleEdges.Add(sphereId);
                    sphere_temp.GetComponent<MagicSphere>().setInitialValues(sphereId, spherePoint);
                }
            }
        }
    }

    private void initializeButtonListeners()
    {
        btnStart.GetComponent<Button>().onClick.AddListener(() => onButtonClick(0));
        btnContinue.GetComponent<Button>().onClick.AddListener(() => onButtonClick(1));
    }

    private void onButtonClick(int id)
    {
        if (id == 0)
        {
            changeGamePhase();
        }
        else if (id == 1)
        {
            changeGamePhase();
        }
    }

    private void changeGamePhase()
    {
        currTime = 0;
        currGamePhase += 1;

        startMenuCanvas.GetComponent<Canvas>().enabled = false;
        endMenuCanvas.GetComponent<Canvas>().enabled = false;
        timerCanvas.GetComponent<Canvas>().enabled = false;

        if (currGamePhase > 2)
        {
            currGamePhase = 0;
        }

        // Actions required at the start of each phase
        if (currGamePhase == 0) // Start
        {
            maxTime = 5;
            restartGame();
            startMenuCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (currGamePhase == 1) // Game action
        {
            maxTime = actionPhaseTime;
            initializeMagicSpheres();
            randomMagicSignTemplate();
            timerCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if(currGamePhase == 2) // Game end
        {
            maxTime = 5;
            isDrawing = false;
            checkMagicSignTemplate();
            destroyMagicGrid();
            endMenuCanvas.GetComponent<Canvas>().enabled = true;
            textScore.GetComponent<TextMeshProUGUI>().text = score.ToString();
        }
        timersChangeCurrTime();
        timersChangeMaxTime();
    }

    public void updateMagicSign(int sphereId, Vector3Int spherePoint)
    {
        if (isDrawing)
        {
            if (currentSphereId >= 0)
            {
                if (getSphereNeighbours(getPointFromId(currentSphereId)).Contains(spherePoint))
                {
                    magicSignAddEdge(sphereId, spherePoint);
                    currentSphereId = sphereId;
                    redrawActiveSpheres();
                }
            }
            else
            {
                currentSphereId = sphereId;
                redrawActiveSpheres();
            }
        }
        else if(Input.GetMouseButton(0))
        {
            startDrawing(sphereId, spherePoint);
        }
    }
    public void startDrawing(int sphereId, Vector3Int spherePoint)
    {
        isDrawing = true;
        SphereConnector[] connectors = GameObject.FindObjectsOfType<SphereConnector>();
        for (int i = 0; i < connectors.Length; i++)
        {
            connectors[i].destroyConnector();
        }
        updateMagicSign(sphereId, spherePoint);
    }

    private void magicSignAddEdge(int sphereId2, Vector3Int spherePoint)
    {
        int sphereId1 = currentSphereId;
        if(sphereId2 < sphereId1)
        {
            int buf = sphereId1;
            sphereId1 = sphereId2;
            sphereId2 = buf;
        }

        Vector2Int newEdge = new Vector2Int(sphereId1, sphereId2);
        if (!magicSignEgdes.Contains(newEdge))
        {
            magicSignEgdes.Add(newEdge);
            drawSphereConnector(newEdge, false);
        }
    }

    private void drawSphereConnector(Vector2Int edge, bool isTemplate)
    {
        Vector3Int spherePoint1 = getPointFromId(edge.x);
        Vector3Int spherePoint2 = getPointFromId(edge.y);
        Vector3 connectorPos = gridStart;
        connectorPos.x += (spherePoint1.x + spherePoint2.x) / 2.0f * magicGridDist;
        connectorPos.y += (spherePoint1.y + spherePoint2.y) / 2.0f * magicGridDist;
        connectorPos.z += (spherePoint1.z + spherePoint2.z) / 2.0f * magicGridDist;

        Vector3 connectorPosStart = gridStart;
        connectorPosStart.x += spherePoint1.x * magicGridDist;
        connectorPosStart.y += spherePoint1.y * magicGridDist;
        connectorPosStart.z += spherePoint1.z * magicGridDist;

        Vector3 connectorPosEnd = gridStart;
        connectorPosEnd.x += spherePoint2.x * magicGridDist;
        connectorPosEnd.y += spherePoint2.y * magicGridDist;
        connectorPosEnd.z += spherePoint2.z * magicGridDist;

        Vector3 size = connectorPosStart - connectorPosEnd;
        float newSize = Mathf.Sqrt(size.x * size.x + size.y * size.y + size.z * size.z) * 0.5f;

        Quaternion connectorRot = Quaternion.FromToRotation(Vector3.up, connectorPosEnd - connectorPosStart);

        GameObject edgeObj = Object.Instantiate(sphereConnectorTemplate, connectorPos, connectorRot);
        size = edgeObj.transform.localScale;
        size.y = newSize;
        edgeObj.transform.localScale = size;
        if(isTemplate)
        {
            edgeObj.GetComponent<SphereConnector>().setTemplate();
        }
    }

    private void redrawActiveSpheres()
    {
        MagicSphere[] spheres = GameObject.FindObjectsOfType<MagicSphere>();
        Vector3Int currSpherePoint = getPointFromId(currentSphereId);
        for (int i = 0; i < spheres.Length; i++)
        {
            MagicSphere sphere = spheres[i];
            if (currentSphereId != -1)
            {
                Vector3Int spherePointTemp = sphere.getSpherePoint();
                if ((spherePointTemp.x == currSpherePoint.x) && (spherePointTemp.y == currSpherePoint.y) && (spherePointTemp.z == currSpherePoint.z))
                {
                    sphere.setActive();
                }
                else if (getSphereNeighbours(currSpherePoint).Contains(spherePointTemp))
                {
                    sphere.setNeighbour();
                }
                else
                {
                    sphere.setInvisible();
                }
            }
            else
            {
                sphere.setInactive();
            }
        }
    }

    private void checkMagicSignTemplate()
    {
        SphereConnector[] connectors = GameObject.FindObjectsOfType<SphereConnector>();
        for (int i = 0; i < connectors.Length; i++)
        {
            connectors[i].destroyConnector();
        }

        MagicSphere[] spheres = GameObject.FindObjectsOfType<MagicSphere>();
        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i].setInactive();
        }

        int edgesMatch = 0;
        for(int i = 0; i < magicSignEgdes.Count; i++)
        {
            if(magicSignTemplate.Contains(magicSignEgdes[i]))
            {
                edgesMatch += 1;
            }
        }

        int pts = edgesMatch;
        int wrongEdges = magicSignEgdes.Count - edgesMatch;
        if(wrongEdges > 0)
        {
            pts -= (wrongEdges - 1) / 2 + 1;
        }

        int spellCase = 3;
        Color textColor = new Color(1.0f, 0.5f, 0.0f);
        if (pts == magicSignTemplate.Count)
        {
            textColor = new Color(0.0f, 1.0f, 0.0f);
            spellCase = 0;
        }
        else if (pts >= magicSignTemplate.Count / 2 + magicSignTemplate.Count % 2)
        {
            textColor = new Color(1.0f, 1.0f, 0.0f);
            spellCase = 1;
        }
        else if (pts <= 0)
        {
            pts = 0;
            textColor = new Color(1.0f, 0.0f, 0.0f);
            spellCase = 2;
        }

        int score_spell = 0;
        if (pts == magicSignTemplate.Count)
        {
            score_spell = spellEdgeScore * magicSignTemplate.Count;
        }
        else
        {
            score_spell = (int)(0.9f * spellEdgeScore * pts);
            
        }

        score += score_spell;
        spellScores.Add(score_spell);

        pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().color = textColor;
        pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().text = pts.ToString() + "/" + magicSignTemplate.Count.ToString();
        pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1.0f, 0.0f, false);
        pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0.0f, 2.5f, false);

        castSpells(spellCase);

        currentSphereId = -1;
        magicSignEgdes = new List<Vector2Int>();
    }

    private void randomMagicSignTemplate()
    {
        int signLen = magicSignLengthMin + Random.Range(0, magicSignLengthVar + 1);
        magicSignTemplate = new List<Vector2Int>();

        int randomSphereId = possibleEdges[Random.Range(0, possibleEdges.Count)];
        Vector3Int spherePoint = getPointFromId(randomSphereId);
        

        int iter = 0;
        while (magicSignTemplate.Count < signLen)
        {
            int sphereId1 = spherePoint.z * magicGridSize * magicGridSize + spherePoint.y * magicGridSize + spherePoint.x;
            List<Vector3Int> neighbours = getSphereNeighbours(spherePoint);
            Vector3Int newSphere = neighbours[Random.Range(0, neighbours.Count)];
            int sphereId2 = newSphere.z * magicGridSize * magicGridSize + newSphere.y * magicGridSize + newSphere.x;

            if (sphereId2 < sphereId1)
            {
                int buf = sphereId1;
                sphereId1 = sphereId2;
                sphereId2 = buf;
            }

            Vector2Int newEdge = new Vector2Int(sphereId1, sphereId2);
            if (!magicSignTemplate.Contains(newEdge))
            {
                magicSignTemplate.Add(newEdge);
                spherePoint = newSphere;
            }

            iter += 1;
            if(iter > 1000)
            {
                break;
            }
        }
        drawMagicSignTemplate();
    }

    private void drawMagicSignTemplate()
    {
        for(int i = 0; i < magicSignTemplate.Count; i++) 
        {
            drawSphereConnector(magicSignTemplate[i], true);
        }
    }

    private List<Vector3Int> getSphereNeighbours(Vector3Int spherePoint)
    {
        List<Vector3Int> neighbours = new List<Vector3Int>();

        for (int z_id = -1; z_id <= 1; z_id++)
        {
            for (int y_id = -1; y_id <= 1; y_id++)
            {
                for (int x_id = -1; x_id <= 1; x_id++)
                {
                    if (z_id != 0 || y_id != 0 || x_id != 0)
                    {
                        Vector3Int neigbourPoint = spherePoint;
                        neigbourPoint.x += x_id;
                        neigbourPoint.y += y_id;
                        neigbourPoint.z += z_id;

                        if((neigbourPoint.x >= 0) && (neigbourPoint.x < magicGridSize) && (neigbourPoint.y >= 0) && (neigbourPoint.y < magicGridSize) &&
                            (neigbourPoint.z >= 0) && (neigbourPoint.z < magicGridSize))
                        {
                            neighbours.Add(neigbourPoint);
                        }
                    }
                }
            }
        }

        return neighbours;
    }

    private void destroyMagicGrid()
    {
        SphereConnector[] connectors = GameObject.FindObjectsOfType<SphereConnector>();
        for (int i = 0; i < connectors.Length; i++)
        {
            connectors[i].destroyConnector();
        }

        MagicSphere[] spheres = GameObject.FindObjectsOfType<MagicSphere>();
        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i].destroySphere();
        }
    }

    private void castSpells(int spellCase)
    {
        int spell_id_enemy = Random.Range(0, spellObjects.Count);
        int spell_id_player = spell_id_enemy + spellCase + 1;
        while (spell_id_player >= spellObjects.Count)
        {
            spell_id_player -= spellObjects.Count;
        }

        float spell_speed_player = -spellSpeed;
        float spell_speed_enemy = -spellSpeed;

        activeSpells = new List<GameObject>();
        // Player spell
        GameObject spell = Object.Instantiate(spellObjects[spell_id_player], playerCastingPoint.GetComponent<Transform>().position, playerCastingPoint.GetComponent<Transform>().rotation);
        spell.GetComponent<csParticleMove>().speed = spell_speed_player;
        activeSpells.Add(spell);

        // Enemy spell
        spell = Object.Instantiate(spellObjects[spell_id_enemy], enemyMageCastingPoint.GetComponent<Transform>().position, enemyMageCastingPoint.GetComponent<Transform>().rotation);
        spell.GetComponent<csParticleMove>().speed = spell_speed_enemy;
        activeSpells.Add(spell);
        spellTimer = spellLifetime;
    }

    private Vector3Int getPointFromId(int sphereId)
    {
        Vector3Int point = new Vector3Int();
        point.z = sphereId / (magicGridSize * magicGridSize);
        point.x = sphereId % magicGridSize;
        point.y = (sphereId - point.z * magicGridSize * magicGridSize - point.x) / magicGridSize;
        return point;
    }

    public void timersChangeMaxTime()
    {
        ManaBarTimer[] timers_list = Object.FindObjectsOfType<ManaBarTimer>();
        for (int i = 0; i < timers_list.Length; i++)
        {
            timers_list[i].setMaxTime(maxTime);
        }
    }

    public void timersChangeCurrTime()
    {
        ManaBarTimer[] timers_list = Object.FindObjectsOfType<ManaBarTimer>();
        for (int i = 0; i < timers_list.Length; i++)
        {
            timers_list[i].setCurrTime(currTime);
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
