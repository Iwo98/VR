using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstrumentsCollide : MonoBehaviour
{
    public GameObject MainGameObject;
    public GameObject Desk;
    public TextMeshProUGUI scoreText;

    private MainGame mainGame;
    private DeskColide deskColide;
    private bool canScore = true;

    public AudioSource InstrumentSound;

    private void Awake()
    {
        mainGame = MainGameObject.GetComponent<MainGame>();
        deskColide = Desk.GetComponent<DeskColide>();

    }

    private void Update()
    {
        if (!deskColide.isPlateInDeskArea)
        {
            canScore = true;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Colide")
        {
            if (deskColide.isPlateInDeskArea)
            {
                if(canScore)
                {
                    mainGame.score++;
                    scoreText.text = mainGame.score.ToString();
                    canScore = false;
                }
            } 
            InstrumentSound.Play();
        }
    }
}
