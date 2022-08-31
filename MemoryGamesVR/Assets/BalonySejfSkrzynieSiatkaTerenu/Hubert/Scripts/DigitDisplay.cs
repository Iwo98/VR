using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitDisplay : MonoBehaviour
{
    int[,] DigitLUT = new int[10, 7]
    {
        {1, 0, 1, 1, 1, 1, 1},
        {0, 0, 0, 0, 1, 0, 1},
        {1, 1, 1, 0, 1, 1, 0},
        {1, 1, 1, 0, 1, 0, 1},
        {0, 1, 0, 1, 1, 0, 1},
        {1, 1, 1, 1, 0, 0, 1},
        {1, 1, 1, 1, 0, 1, 1},
        {1, 0, 0, 0, 1, 0, 1},
        {1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 0, 1}
    };

    private List<MeshRenderer> segments;

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<MeshRenderer>(gameObject.GetComponentsInChildren<MeshRenderer>());
        segments.RemoveAt(0);
    }

    public void UpdateDigit(int number)
    {
        for (int i = 0; i < segments.Count && i < 7; i++)
            segments[i].enabled = DigitLUT[number, i] == 1; 
    }
}
