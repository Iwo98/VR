using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CopyMesh : MonoBehaviour
{
    public GameObject parent;
    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!mesh)
            TryInitialize();
    }

    void TryInitialize()
    {
        if (parent)
        {
            Debug.Log("There is parent");
            mesh = new Mesh();
            mesh.vertices = parent.GetComponent<MeshFilter>().mesh.vertices;
            mesh.triangles = parent.GetComponent<MeshFilter>().mesh.triangles;
            mesh.RecalculateNormals();
            if (mesh)
            {
                Debug.Log("It has mesh");
                GetComponent<MeshFilter>().mesh = mesh;
            }
                
        }
        
    }
}
