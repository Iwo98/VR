using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : Level
{
    public override void SpawnCandles(GameObject singleChandelier, GameObject tripleChandelier)
    {
        numberOfCandles = 7;
        numberOfStates = 2;
        base.SpawnCandles(singleChandelier, tripleChandelier);
        Instantiate(singleChandelier, new Vector3(0, 0.807f, -0.106f), Quaternion.Euler(-90f, 90f, 0f));

        Instantiate(tripleChandelier, new Vector3(-0.75f, 0.807f, -0.85f), Quaternion.Euler(-90f, 90f, 90f));
        Instantiate(tripleChandelier, new Vector3(0.75f, 0.807f, -0.85f), Quaternion.Euler(-90f, 90f, 90f));
    }
}
