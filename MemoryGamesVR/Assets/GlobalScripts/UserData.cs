using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UserData : MonoBehaviour
{
    // ============ Game data class ================
    [System.Serializable]
    public class GameData
    {
        public string name = "";
        public int avatar_id = 0;
        public List<Scores> gameScores;
        public List<int> trainingAvailableGames;
        public int lastChosenGame;

        [System.Serializable]
        public class Scores
        {
            public string currGameName = "";
            public List<float> currGameScores;

            public Scores(string gameName)
            {
                currGameName = gameName;
                currGameScores = new List<float>();
            }
        }


        public GameData(string userName)
        {
            name = userName;
            lastChosenGame = -1;
            avatar_id = 0;
            trainingAvailableGames = new List<int>();
            initScoreLists();
        }


        private void initScoreLists()
        {
            ConstantGameValues game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
            gameScores = new List<Scores>();
            gameScores.Add(new Scores("Total"));
            for(int i = 0; i < game_values.numberOfGames; i++)
            {
                gameScores.Add(new Scores(game_values.gameIdNames[i]));
            }
        }

        private void initTrainingGameList()
        {
            ConstantGameValues game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
            trainingAvailableGames = new List<int>();
            for(int i = 0; i < game_values.numberOfGames; i++)
            {
                trainingAvailableGames.Add(i);
            }
        }

        public void AddScore(int id, float score)
        {
            gameScores[id].currGameScores.Add(score);
        }
    }

    // ============== Save and Load methods =================
    public GameData data;

    void Start()
    {
        ResetData();
        LoadFile();
    }

    public void ResetData()
    {
        data = new GameData(PlayerPrefs.GetString("username"));
    }

    public void SaveFile()
    {
        string currentName = PlayerPrefs.GetString("username");
        string destination = Application.persistentDataPath + "/" + currentName + ".dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public int LoadFile()
    {
        ResetData();
        string currentName = PlayerPrefs.GetString("username");
        string destination = Application.persistentDataPath + "/" + currentName + ".dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.Log("User save not found.");
            return 1;
        }

        BinaryFormatter bf = new BinaryFormatter();
        data = (GameData)bf.Deserialize(file);
        file.Close();
        return 0;
    }


}
