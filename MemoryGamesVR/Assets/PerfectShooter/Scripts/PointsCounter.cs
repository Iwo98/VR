using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    public int points = 0;
    public GameObject temp = null;
    public int score = 0;
    // Start is called before the first frame update
    

    public string GetPoints()
    {
        return (points * 10/ temp.GetComponent<TargetPosition>().maxPoints).ToString() + "%";
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
        //int difficulty = GameObject.FindObjectsOfType<TargetPosition>()[0].diffLevel;
        int difficulty = TargetPosition.diffLevel;
        score = points * 10 / temp.GetComponent<TargetPosition>().maxPoints;
        Debug.Log(score);
        //score *= (int)Mathf.RoundToInt(1 + (difficulty - 1) * 0.3f);  //  Change for difficulty
        
        GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0];
        game_manager.endGameManagement(score);
    }
}
