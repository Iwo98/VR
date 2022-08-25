using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputReader : MonoBehaviour
{   [SerializeField]
    public TextMeshPro tekscik;
    // Start is called before the first frame update
    void Start()
    {
        //tekscik = GetComponent<TextMeshPro>();
        tekscik.text = "testowanie";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
