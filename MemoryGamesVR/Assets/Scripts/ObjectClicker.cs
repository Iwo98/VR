using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Vector3 fromWhere = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(fromWhere);

        if (Physics.Raycast(ray, out hit, 100.0f)) {
            if (hit.transform) {
                PrintName(hit.transform.gameObject);
            }
        }
    }

    private void PrintName(GameObject go)
    {
        print(go.name);
    }
}
