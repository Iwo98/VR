using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFigure : MonoBehaviour
{
    public int n = 3;
    public int[,,] matrix;
    public Material wall;
    public Material nowall;
    public GameObject top, bottom, left, right, front, back;

    void Generate()
    {
        int[,] ans = new int[n, n];

        for (int x = 0; x < n; x++)
           for (int y = 0; y < n; y++)
                for (int z = 0; z < n; z++)
                {
                    if (matrix[x, y, z] == 0)
                        ans[x, y] = 1;
                }

        for (int x = 0; x < n; x++)
            for (int y = 0; y < n; y++)
            {
                GameObject brush = GameObject.CreatePrimitive(PrimitiveType.Cube);
                brush.transform.localScale = new Vector3(1, 1, 0.1f);
                brush.transform.localPosition = new Vector3(x - 1, y - 1+2, 2);

                if (ans[x, y] == 1)
                    brush.GetComponent<Renderer>().material = wall;
                else
                    brush.GetComponent<Renderer>().material = nowall;
                

                Instantiate(brush).transform.SetParent(front.transform);
                brush.transform.localPosition = new Vector3(x - 1, y - 1+2, -3);
                Instantiate(brush).transform.SetParent(back.transform);
                Destroy(brush);
            }

        ans = new int[n, n];

        for (int x = 0; x < n; x++)
            for (int z = 0; z < n; z++)
                for (int y = 0; y < n; y++)
                {
                    if (matrix[x, y, z] == 0)
                        ans[x, z] = 1;
                }

        for (int x = 0; x < n; x++)
            for (int y = 0; y < n; y++)
            {
                GameObject brush = GameObject.CreatePrimitive(PrimitiveType.Cube);
                brush.transform.localScale = new Vector3(1, 0.1f, 1);
                brush.transform.localPosition = new Vector3(x - 1, 3+2, y - 1);

                if (ans[x, y] == 1)
                    brush.GetComponent<Renderer>().material = wall;
                else
                    brush.GetComponent<Renderer>().material = nowall;

                Instantiate(brush).transform.SetParent(top.transform);
                brush.transform.localPosition = new Vector3(x - 1, -3+2, y - 1);
                Instantiate(brush).transform.SetParent(bottom.transform);
                Destroy(brush);
            }

        ans = new int[n, n];

        for (int y = 0; y < n; y++)
            for (int z = 0; z < n; z++)
                for (int x = 0; x < n; x++)
                {
                    if (matrix[x, y, z] == 0)
                        ans[y, z] = 1;
                }

        for (int x = 0; x < n; x++)
            for (int y = 0; y < n; y++)
            {
                GameObject brush = GameObject.CreatePrimitive(PrimitiveType.Cube);
                brush.transform.localScale = new Vector3(0.1f, 1, 1);
                brush.transform.position = new Vector3(2, x - 1+2, y - 1);

                if (ans[x, y] == 1)
                    brush.GetComponent<Renderer>().material = wall;
                else
                    brush.GetComponent<Renderer>().material = nowall;

                Instantiate(brush).transform.SetParent(right.transform);
                brush.transform.position = new Vector3(-3, x - 1+2, y - 1);
                Instantiate(brush).transform.SetParent(left.transform);
                Destroy(brush);
            }

        
    }
}
