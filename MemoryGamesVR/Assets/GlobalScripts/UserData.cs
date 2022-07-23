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

        public List<int> chooseTrainingGame()
        {
            int game_id = 0;
            int difficulty = 1;
            bool game_chosen = false;
            // Game choice
            if (trainingAvailableGames.Count == 0)
            {
                initTrainingGameList();
            }

            while (game_chosen == false)
            {
                int rand_id = Random.Range(0, trainingAvailableGames.Count);
                game_id = trainingAvailableGames[rand_id];
                if (game_id != lastChosenGame)
                {
                    trainingAvailableGames.RemoveAt(rand_id);
                    game_chosen = true;
                }
            }

            lastChosenGame = game_id;

            //Difficulty choice
            difficulty = getGameDifficulty(game_id);

            List<int> game_vals = new List<int>();
            game_vals.Add(game_id);
            game_vals.Add(difficulty);
            return game_vals;
        }

        public void AddScore(int id, float score)
        {
            gameScores[id].currGameScores.Add(score);
        }

        public int getGameDifficulty(int id)  // Choose game difficulty based on results from last 5 games
        {
            List<float> game_scores = gameScores[id + 1].currGameScores;
            int max_scores = game_scores.Count;
            int num_scores = max_scores;
            if (max_scores > 5)
                max_scores = 5;
            List<float> diff_scores = new List<float>() { 0.0f, 725.0f, 825.0f, 925.0f, 1025.0f };
            for(int i = 0; i < max_scores; i++)
            {
                diff_scores[i] = game_scores[num_scores - i - 1];
            }

            List<float> diff_weights = new List<float>() { 1.0f, 0.8f, 0.6f, 0.4f, 0.2f };
            float weights_sum = 3.0f;
            float weighted_scores_sum = 0.0f;
            for(int i = 0; i < diff_scores.Count; i++)
            {
                weighted_scores_sum += diff_scores[i] * diff_weights[i];
            }
            weighted_scores_sum /= weights_sum;

            float first_diff_score = 750.0f;
            float diff_score_change = 0.3f;

            weighted_scores_sum -= first_diff_score;
            weighted_scores_sum /= first_diff_score * diff_score_change;
            weighted_scores_sum = Mathf.Clamp(weighted_scores_sum, -0.9f, 8.9f);
            int difficulty = (int)Mathf.Ceil(weighted_scores_sum) + 1;

            return difficulty;
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
