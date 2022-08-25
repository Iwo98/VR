using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFigure1 : MonoBehaviour
{
    public static int n = 3;
    public Material material;
    public int[,,] matrix = new int[n, n, n];
    public GameObject[,,] figure = new GameObject[n, n, n];
    

    void Generate()
    {
        for (int x = 0; x < n; x++)
            for (int z = 0; z < n; z++)
                for (int y = 0; y < n; y++)
                    if (matrix[x, y, z] == 0)
                    {
                        figure[x, y, z] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        figure[x, y, z].transform.SetParent(gameObject.transform);
                        figure[x, y, z].transform.localScale = new Vector3(1, 1, 1);
                        figure[x, y, z].transform.localPosition = new Vector3(x - 1, y, z - 1);
                        figure[x, y, z].GetComponent<Renderer>().material = material;
                    }
    }
}
