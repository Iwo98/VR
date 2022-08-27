using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verticles;
    int[] plane;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
    }

    void CreateShape()
    {
        verticles = new Vector3[3]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3((mesh.vertices.Length - 1) / 2, 0, 0),
        };

        plane = new int[]
        {
            0, 1, 2
        };
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticles;
        mesh.triangles = plane;
    }
}
