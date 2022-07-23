using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    public int points = 0;
    public GameObject temp = null;
    // Start is called before the first frame update
    

    public string GetPoints()
    {
        return (points*1300/temp.GetComponent<TargetPosition>().maxPoints).ToString();
    }

    public  void UpPoints(int value = 1)
    {
        this.points += value;
    }

    public void DownPoints(int value = 1)
    {
        this.points -= value;
    }

    public void btnClickGameEnd()
    {
        int difficulty = GameObject.FindObjectsOfType<TargetPosition>()[0].diffLevel;
        float score = points * 1300 / temp.GetComponent<TargetPosition>().maxPoints;
        score *= (1.0f + (difficulty - 1) * 0.3f);  //  Change for difficulty
        GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        game_manager.endGameManagement(score);
    }
}
