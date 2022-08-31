using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantGameValues : MonoBehaviour
{
    public int numberOfGames;
    public int trainingNumberOfGames;
    public int cognitiveTrainingNumberOfGames;
    public int exerciseTrainingNumberOfGames;
    public int warmUpNumberOfGames;
    public int maxDifficulty;
    public List<string> gameIdNames;
    public List<string> gameNames;
    public List<string> gameScenes;
    public List<string> gameIcons2DPaths;
    public List<string> avatarSpritesPaths;
    public List<string> cognitiveGameNames;
    public List<string> exerciseGameNames;


    // Start is called before the first frame update
    void Start()
    {
        initAllValues();
    }

    public void initAllValues()
    {
        initVals();
        initIdNames();
        initNames();
        initScenes();
        initGameIcons2D();
        initAvatars();
        initCognitiveGameNames();
        initExerciseGameNames();
    }

    private void initVals()
    {
        numberOfGames = 10;
        cognitiveTrainingNumberOfGames = 8;
        exerciseTrainingNumberOfGames = 8;
        warmUpNumberOfGames = 2;
        trainingNumberOfGames = 1;
        maxDifficulty = 10;
    }

    private void initIdNames()
    {
        gameIdNames = new List<string>();
        gameIdNames.Add("Candles");
        gameIdNames.Add("Alchemist");
        gameIdNames.Add("MagicDuel");
        gameIdNames.Add("PerfectShooter");
        gameIdNames.Add("WhatsNew");
        gameIdNames.Add("DungeonEscape");
        gameIdNames.Add("VanishingThings");
        gameIdNames.Add("JumpingJackGame");
        gameIdNames.Add("AbdominalsGame");
        gameIdNames.Add("SitUpGame");
    }

    private void initNames()
    {
        gameNames = new List<string>();
        gameNames.Add("Świeczniki");
        gameNames.Add("Uczeń alchemika");
        gameNames.Add("Magiczny pojedynek");
        gameNames.Add("Strzelec wyborowy");
        gameNames.Add("Co doszło?");
        gameNames.Add("Ucieczka z lochu");
        gameNames.Add("Znikające przedmioty");
        gameNames.Add("Pajacyki");
        gameNames.Add("Brzuszki");
        gameNames.Add("Przysiady");
    }

    private void initCognitiveGameNames()
    {
        cognitiveGameNames = new List<string>();
        cognitiveGameNames.Add("Świeczniki");
        cognitiveGameNames.Add("Uczeń alchemika");
        cognitiveGameNames.Add("Magiczny pojedynek");
        cognitiveGameNames.Add("Strzelec wyborowy");
        cognitiveGameNames.Add("Co doszło?");
        cognitiveGameNames.Add("Ucieczka z lochu");
        cognitiveGameNames.Add("Znikające przedmioty");
    }

    private void initExerciseGameNames()
    {
        exerciseGameNames = new List<string>();
        exerciseGameNames.Add("Pajacyki");
        exerciseGameNames.Add("Brzuszki");
        exerciseGameNames.Add("Przysiady");
    }

    private void initScenes()
    {
        gameScenes = new List<string>();
        gameScenes.Add("Candles_Menu/Scenes/Swieczniki");
        gameScenes.Add("AlchemistGame/Scenes/AlchemistGame");
        gameScenes.Add("MagicDuelGame/Scenes/MagicDuelGame");
        gameScenes.Add("PerfectShooter/PerfectShooter");
        gameScenes.Add("WhatsNew/WhatsNew");
        gameScenes.Add("MazeRunner/Scenes/MazeRunner");
        gameScenes.Add("VanishingThings/Scenes/VanishingThings");
        gameScenes.Add("Pajacyki_Game/Scenes/Pajacyki_Game");
        gameScenes.Add("Abdominals_Game/Scenes/Abdominals_Game");
        gameScenes.Add("Situp_Game/Scenes/Situp_Game");
    }

    private void initGameIcons2D()
    {
        gameIcons2DPaths = new List<string>();
        gameIcons2DPaths.Add("Sprites/GameIcons/torch");
        gameIcons2DPaths.Add("Sprites/GameIcons/potion");
        gameIcons2DPaths.Add("Sprites/GameIcons/fireball");
        gameIcons2DPaths.Add("Sprites/GameIcons/bullseye");
        gameIcons2DPaths.Add("Sprites/GameIcons/shelf");
        gameIcons2DPaths.Add("Sprites/GameIcons/door");
        gameIcons2DPaths.Add("Sprites/GameIcons/sword_and_axe");
        gameIcons2DPaths.Add("Sprites/GameIcons/cymbals");
        gameIcons2DPaths.Add("Sprites/GameIcons/sh");
        gameIcons2DPaths.Add("Sprites/GameIcons/crown");
    }

    public void initAvatars()
    {
        avatarSpritesPaths = new List<string>();
        avatarSpritesPaths.Add("Sprites/Avatars/avatar1");
        avatarSpritesPaths.Add("Sprites/Avatars/avatar2");
        avatarSpritesPaths.Add("Sprites/Avatars/avatar3");
        avatarSpritesPaths.Add("Sprites/Avatars/avatar4");
        avatarSpritesPaths.Add("Sprites/Avatars/avatar5");
        avatarSpritesPaths.Add("Sprites/Avatars/avatar6");
        avatarSpritesPaths.Add("Sprites/Avatars/avatar7");
        avatarSpritesPaths.Add("Sprites/Avatars/avatar8");
        avatarSpritesPaths.Add("Sprites/Avatars/avatar9");
        avatarSpritesPaths.Add("Sprites/Avatars/avatar10");
    }
}
