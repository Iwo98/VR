using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHints : MonoBehaviour
{
    public TextMeshProUGUI TooHighText;
    public TextMeshProUGUI TooLowText;
    public GameObject MainCamera;
    private CollisionDetector collisionDetector;
    // Start is called before the first frame update
    void Start()
    {
        collisionDetector = MainCamera.GetComponent<CollisionDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(collisionDetector.tooHigh)
        {
            TooHighText.text = "Jesteœ za wysoko";
        }
        else
        {
            TooHighText.text = "";
        }

        if (collisionDetector.tooLow)
        {
            TooLowText.text = "Jesteœ za nisko";
        }
        else
        {
            TooLowText.text = "";
        }

    }
}

