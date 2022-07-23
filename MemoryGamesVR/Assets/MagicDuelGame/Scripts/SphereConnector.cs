using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereConnector : MonoBehaviour
{
    public Material templateMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyConnector()
    {
        Destroy(gameObject);
    }

    public void setTemplate()
    {
        gameObject.GetComponent<MeshRenderer>().material = templateMaterial;
        transform.Find("Sphere").GetComponent<MeshRenderer>().material = templateMaterial;
        transform.Find("Sphere1").GetComponent<MeshRenderer>().material = templateMaterial;
    }
}
