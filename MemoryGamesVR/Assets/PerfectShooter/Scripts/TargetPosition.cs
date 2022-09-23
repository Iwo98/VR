using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotation = new List<Quaternion>();

    public List<Transform> activeShields = new List<Transform>();
    private int numberOfTargets = 6;
    private int numberOfColors = 2;
    public static int diffLevel;
    public int maxPoints = 0;

    private void selectdiffLevel(int level)
    {
        switch (level - 1)
        {
            case 0:
                numberOfTargets = 4;
                numberOfColors = 2;
                break;
            case 1:
                numberOfTargets = 5;
                numberOfColors = 2;
                break;
            case 2:
                numberOfTargets = 6;
                numberOfColors = 2;
                break;
            case 3:
                numberOfTargets = 7;
                numberOfColors = 2;
                break;
            case 4:
                numberOfTargets = 7;
                numberOfColors = 3;
                break;
            case 5:
                numberOfTargets = 8;
                numberOfColors = 3;
                break;
            case 6:
                numberOfTargets = 9;
                numberOfColors = 3;
                break;
            case 7:
                numberOfTargets = 10;
                numberOfColors = 3;
                break;
            case 8:
                numberOfTargets = 11;
                numberOfColors = 3;
                break;
            case 9:
                numberOfTargets = 12;
                numberOfColors = 3;
                break;
            default:
                numberOfTargets = 6;
                numberOfColors = 2;
                break;
        }
        maxPoints = numberOfTargets;

    }


    // Local variables
    private GameObject TargetsObject, TargetChildObject;
    
    

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("curr_game_difficulty"))
        {
            diffLevel = PlayerPrefs.GetInt("curr_game_difficulty");
        }
        selectdiffLevel(diffLevel);
        createListOfPositions();
    }

    // Update is called once per frame
    void Update()
    {
        selectdiffLevel(diffLevel);
    }

    private void createListOfPositions()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            positions.Add(this.transform.GetChild(i).position);
            rotation.Add(this.transform.GetChild(i).rotation);

            this.transform.GetChild(i).position = new Vector3(0f, 0f, 35f);
            this.transform.GetChild(i).rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    public void _setChildPosition()
    {
        List<int> shufled_position = RandomizeList(numberOfTargets);
        List<int> shufled_target = RandomizeList(numberOfTargets);
        GameObject myObject = GameObject.Find("Better_Targets");
        for (int i = 0; i < numberOfTargets; i++)
        {
            myObject.transform.GetChild(shufled_target[i]).transform.position = positions[shufled_position[i]];
            myObject.transform.GetChild(shufled_target[i]).transform.rotation = rotation[shufled_position[i]];

            activeShields.Add(myObject.transform.GetChild(shufled_target[i]));
        }
    }
    private IEnumerator Wait(float time = 0)
    {
        yield return new WaitForSeconds(time);
    }

    private List<int> RandomizeList(int size = 1)
    {
        int count = this.numberOfColors*4;
        List<int> shufle = new List<int>();
        while(shufle.Count < size)
        {
            int temp = Random.Range(0, count);
            if (!shufle.Contains(temp))
            {
                shufle.Add(temp);
            }
        }
        return shufle;
    }
}
