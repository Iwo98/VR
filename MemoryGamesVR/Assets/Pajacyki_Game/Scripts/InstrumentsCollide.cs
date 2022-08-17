using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentsCollide : MonoBehaviour
{
    public GameObject MainGameObject;
    public GameObject Desk;

    private MainGame mainGame;
    private DeskColide deskColide;

    public AudioSource InstrumentSound;

    private void Awake()
    {
        mainGame = MainGameObject.GetComponent<MainGame>();
        deskColide = Desk.GetComponent<DeskColide>();

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Colide")
        {
            if (deskColide.isPlateInDeskArea)
            {
                mainGame.score++;

            }
            InstrumentSound.Play();
        }
    }
}
