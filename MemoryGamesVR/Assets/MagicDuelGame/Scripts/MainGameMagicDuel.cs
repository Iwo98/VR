using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainGameMagicDuel : MonoBehaviour
{
    public int difficulty;
    public int actionPhaseTime;

    private int maxDifficulty = 10;
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
    private int spellEdgeScore = 22;
    private float difficulty_score_mul = 0.3f;
    private float scoreToDmgMultiplier = 5.775f;

    private int currGamePhase = 3;
    private float currTime = 0;
    private float maxTime = 60;

    private float spellSpeed = 0.08f;
    private bool isSpellActive = false;
    private List<GameObject> activeSpells;
    private List<int> spellQueue = new List<int>();
    private List<int> spellScoresQueue = new List<int>();

    public bool isRightHand = true;  // if false use left hand

    public GameObject magicSphereTemplate, sphereConnectorTemplate;

    public List<GameObject> spellObjects;
    public List<GameObject> explosionObjects;
    public GameObject playerCastingPoint, enemyMageCastingPoint;

    public GameObject startMenuCanvas, endMenuCanvas, timerCanvas, pointsCanvas, dmgCanvas;
    public GameObject textScore, textDamage;

    public AudioSource audioSource1, audioSource2;
    public AudioClip spellCastAudio;
    public AudioClip spellExplosionAudio;

    public GameObject mage;
    private Animator mageAnimator;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
        {
            difficulty = PlayerPrefs.GetInt("curr_game_difficulty");
        }
        mageAnimator = mage.GetComponent<Animator>();

        currGamePhase = -1;
        changeGamePhase();
    }


    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if(currGamePhase == 1)
        {

            if (isDrawing && !Input.GetMouseButton(0) && !isVRTriggerPressed())
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

        // Cast spells if there are no other spells active
        if((spellQueue.Count > 0) && !isSpellActive)
        {
            isSpellActive = true;
            int castType = spellQueue[0];
            spellQueue.RemoveAt(0);
            castSpells(castType);
        }
    }

    // Performed at the start of each game
    private void restartGame()
    {
        dmgCanvas.GetComponent<Transform>().Find("dmgImg").GetComponent<Image>().enabled = false;
        initializeGameValues();
    }

    // Initialize game variables, based on difficulty
    private void initializeGameValues()
    {
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

        switch(difficulty)
        {
            case 1:
                magicSignLengthMin = 3;
                magicSignLengthVar = 0;
                magicGridSize = 2;
                break;
            case 2:
                magicSignLengthMin = 4;
                magicSignLengthVar = 0;
                magicGridSize      = 2;
                break;
            case 3:
                magicSignLengthMin = 4;
                magicSignLengthVar = 1;
                magicGridSize      = 2;
                break;
            case 4:
                magicSignLengthMin = 5;
                magicSignLengthVar = 1;
                magicGridSize      = 2;
                break;
            case 5:
                magicSignLengthMin = 5;
                magicSignLengthVar = 0;
                magicGridSize      = 3;
                break;
            case 6:
                magicSignLengthMin = 5;
                magicSignLengthVar = 1;
                magicGridSize      = 3;
                break;
            case 7:
                magicSignLengthMin = 6;
                magicSignLengthVar = 1;
                magicGridSize      = 3;
                break;
            case 8:
                magicSignLengthMin = 6;
                magicSignLengthVar = 2;
                magicGridSize      = 3;
                break;
            case 9:
                magicSignLengthMin = 7;
                magicSignLengthVar = 2;
                magicGridSize      = 3;
                break;
            default:  // Difficulty 10
                magicSignLengthMin = 8;
                magicSignLengthVar = 2;
                magicGridSize      = 3;
                break;
        }

        float gridDistCoeff = 3.4f;
        if (magicGridSize == 3)
        {
            gridDistCoeff = 2.7f;
        }
        magicGridDist = gridDistCoeff * magicSphereTemplate.GetComponent<MeshFilter>().transform.localScale.x;

        score = 0;
        currentSphereId = -1;
        isDrawing = false;
        magicSignTemplate = new List<Vector2Int>();
        magicSignEgdes = new List<Vector2Int>();
        isSpellActive = false;
        spellQueue = new List<int>();
        spellScoresQueue = new List<int>();
    }

    // Draw magic grid
    private void initializeMagicSpheres()
    {
        MagicSphere[] oldSpheres = GameObject.FindObjectsOfType<MagicSphere>();
        for (int i = 0; i < oldSpheres.Length; i++)
        {
            Destroy(oldSpheres[i]);
        }

        gridStart = GameObject.Find("MagicSpheresStart").transform.position;
        float dist_half = magicGridDist * (magicGridSize - 1) / 2;
        gridStart.x -= dist_half;
        gridStart.y -= dist_half;

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

    // Buttons
    public void btnStartClick(bool isRight)
    {
        isRightHand = isRight;
        changeGamePhase();
    }

    public void btnEndClick()
    {
        GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        game_manager.endGameManagement(score);
    }

    // Performed at the start of each game phase(start menu, action phase, end menu etc.)
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
            isSpellActive = false;
            spellQueue = new List<int>();
            maxTime = actionPhaseTime;
            initializeMagicSpheres();
            randomMagicSignTemplate();
            timerCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if(currGamePhase == 2) // Game end
        {
            maxTime = 5;
            isDrawing = false;
            if(magicSignEgdes.Count > 0)
                checkMagicSignTemplate();
            destroyMagicGrid();
            isSpellActive = false;
            spellQueue = new List<int>();
            //score = (int)(score * (1.0f + difficulty_score_mul * (difficulty - 1)));
            score = Mathf.RoundToInt(score * 100 / 1200);
            if (score > 100)
            {
                score = 100;
            }
            endMenuCanvas.GetComponent<Canvas>().enabled = true;
            textScore.GetComponent<TextMeshProUGUI>().text = score.ToString();
        }
        timersChangeCurrTime();
        timersChangeMaxTime();
    }

    // 
    public void updateMagicSign(int sphereId, Vector3Int spherePoint)
    {
        VRHandInputManager vr_input_manager = GameObject.FindObjectsOfType<VRHandInputManager>()[0];
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
        else if(Input.GetMouseButton(0) || vr_input_manager.rightTriggerPressed == true)
        {
            startDrawing(sphereId, spherePoint);
        }
    }


    public void startDrawing(int sphereId, Vector3Int spherePoint)
    {
        if (isDrawing == false)
        {
            isDrawing = true;
            SphereConnector[] connectors = GameObject.FindObjectsOfType<SphereConnector>();
            for (int i = 0; i < connectors.Length; i++)
            {
                connectors[i].destroyConnector();
            }
            updateMagicSign(sphereId, spherePoint);
        }
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

    // Draw edges of magic sign
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

    // Check magic sign when player ends drawing
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
        
        pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().color = textColor;
        pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().text = pts.ToString() + "/" + magicSignTemplate.Count.ToString();
        pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1.0f, 0.0f, false);
        pointsCanvas.GetComponent<Transform>().Find("pointsText").GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0.0f, 2.5f, false);

        spellQueue.Add(spellCase);
        spellScoresQueue.Add(score_spell);

        currentSphereId = -1;
        magicSignEgdes = new List<Vector2Int>();
    }

    // Random new magic sign
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

    // Cast visual representation of spells, based on score
    private void castSpells(int spellCase)
    {
        audioSource1.clip = spellCastAudio;
        audioSource1.volume = 0.5f;
        audioSource1.Play();

        int spell_id_enemy = Random.Range(0, spellObjects.Count);
        int spell_id_player = spell_id_enemy + spellCase + 1;
        while (spell_id_player >= spellObjects.Count)
        {
            spell_id_player -= spellObjects.Count;
        }

        float spell_speed_player = -spellSpeed;
        float spell_speed_enemy = -spellSpeed;
        switch (spellCase)
        {
            case 0:
                spell_speed_player *= 2.5f;
                spell_speed_enemy *= 1.0f;
                break;
            case 1:
                spell_speed_player *= 2.0f;
                spell_speed_enemy *= 1.0f;
                break;
            case 2:
                spell_speed_player *= 0.95f;
                spell_speed_enemy *= 1.0f;
                break;
            case 3:
                spell_speed_player *= 1.5f;
                spell_speed_enemy *= 1.0f;
                break;
            default:
                spell_speed_player *= 1.0f;
                spell_speed_enemy *= 1.0f;
                break;
        }

        activeSpells = new List<GameObject>();
        // Player spell
        GameObject spell = Object.Instantiate(spellObjects[spell_id_player], playerCastingPoint.GetComponent<Transform>().position, playerCastingPoint.GetComponent<Transform>().rotation);
        spell.GetComponent<csParticleMove>().speed = spell_speed_player;
        spell.GetComponent<destroyEffect>().setSpellType(true);  // Set value on SpellCollider child
        spell.GetComponent<destroyEffect>().setAutomaticDestroy(3.0f);
        activeSpells.Add(spell);

        // Enemy spell
        mageAnimator.Play("Attack1");
        StartCoroutine(castEnemySpell(spell_id_enemy, spell_speed_enemy, 0.25f));  // Wait after animation
    }

    IEnumerator castEnemySpell(int spell_id_enemy, float spell_speed_enemy, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject spell = Object.Instantiate(spellObjects[spell_id_enemy], enemyMageCastingPoint.GetComponent<Transform>().position, enemyMageCastingPoint.GetComponent<Transform>().rotation);
        spell.GetComponent<csParticleMove>().speed = spell_speed_enemy;
        spell.GetComponent<destroyEffect>().setAutomaticDestroy(3.0f);
        activeSpells.Add(spell);
    }

    // Destroy spells when they collide
    public void destroySpells()
    {
        Vector3 explPos = activeSpells[0].GetComponent<Transform>().position;
        for (int i = 0; i < activeSpells.Count; i++)
        {
            activeSpells[i].GetComponent<destroyEffect>().destroy();
        }

        int explId = Random.Range(0, explosionObjects.Count);
        GameObject explosion = Object.Instantiate(explosionObjects[explId], explPos, Quaternion.identity);
        explosion.GetComponent<destroyEffect>().setAutomaticDestroy(2.0f);
        audioSource2.clip = spellExplosionAudio;
        audioSource2.volume = 0.25f;
        audioSource2.Play();

        dmgCanvas.GetComponent<Transform>().Find("dmgText").GetComponent<TextMeshProUGUI>().text = ((int)(spellScoresQueue[0] * scoreToDmgMultiplier)).ToString();
        dmgCanvas.GetComponent<Transform>().Find("dmgText").GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1.0f, 0.0f, false);
        dmgCanvas.GetComponent<Transform>().Find("dmgText").GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0.0f, 2.5f, false);
        dmgCanvas.GetComponent<Transform>().Find("dmgImg").GetComponent<Image>().enabled = true;
        dmgCanvas.GetComponent<Transform>().Find("dmgImg").GetComponent<Image>().CrossFadeAlpha(1.0f, 0.0f, false);
        dmgCanvas.GetComponent<Transform>().Find("dmgImg").GetComponent<Image>().CrossFadeAlpha(0.0f, 2.5f, false);

        spellScoresQueue.RemoveAt(0);
        isSpellActive = false;
    }

    // Get position in grid of magic sphere, from its id
    private Vector3Int getPointFromId(int sphereId)
    {
        Vector3Int point = new Vector3Int();
        point.z = sphereId / (magicGridSize * magicGridSize);
        point.x = sphereId % magicGridSize;
        point.y = (sphereId - point.z * magicGridSize * magicGridSize - point.x) / magicGridSize;
        return point;
    }

    // Changing timers
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

    public bool isVRTriggerPressed()
    {
        VRHandInputManager vr_input_manager = GameObject.FindObjectsOfType<VRHandInputManager>()[0];
        if (isRightHand && vr_input_manager.rightTriggerPressed)
            return true;
        else if (!isRightHand && vr_input_manager.leftTriggerPressed)
            return true;
        return false;
    }
}
