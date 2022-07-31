using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectGames : MonoBehaviour
{
    public Canvas SelectGameCanvas;
    public TextMeshProUGUI GameNameTemplate;

    private GameObject Background;
    private ConstantGameValues game_values;

    // Start is called before the first frame update
    void Start()
    {
        game_values = GameObject.FindObjectsOfType<ConstantGameValues>()[0];
        game_values.initAllValues();
        Background = GameObject.Find("ImageBg");

        int index = 0;
        foreach (string gameName in game_values.gameNames)
        {
            TextMeshProUGUI newGameName = Instantiate<TextMeshProUGUI>(GameNameTemplate, Background.transform, true);

            newGameName.name = index.ToString();
            newGameName.transform.SetParent(Background.transform);
            newGameName.transform.Translate(0,-0.36f * (index + 1), 0);

            newGameName.text = gameName;
            index++;
        }

        Destroy(GameNameTemplate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
