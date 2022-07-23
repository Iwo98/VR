using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    private string textValue;
    public GameObject textElement;
    

    // Update is called once per frame
    void Update()
    {
        textValue = this.GetComponent<PointsCounter>().GetPoints().ToString();
        textElement.GetComponent<TMPro.TextMeshProUGUI>().text = textValue;
    }
}
