using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : Level
{
    public override void SpawnCandles(GameObject singleChandelier, GameObject tripleChandelier)
    {
        numberOfCandles = 8;
        numberOfStates = 3;
        base.SpawnCandles(singleChandelier, tripleChandelier);
        Instantiate(singleChandelier, new Vector3(0.29f, 0.807f, -0.106f), Quaternion.Euler(-90f, 90f, 0f));
        Instantiate(singleChandelier, new Vector3(-0.29f, 0.807f, -0.106f), Quaternion.Euler(-90f, 90f, 0f));

        Instantiate(tripleChandelier, new Vector3(-0.75f, 0.807f, -0.85f), Quaternion.Euler(-90f, 90f, 90f));
        Instantiate(tripleChandelier, new Vector3(0.75f, 0.807f, -0.85f), Quaternion.Euler(-90f, 90f, 90f));
    }
}
