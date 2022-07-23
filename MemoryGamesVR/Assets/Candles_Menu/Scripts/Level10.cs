using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level10 : Level
{
    public override void SpawnCandles(GameObject singleChandelier, GameObject tripleChandelier)
    {
        numberOfCandles = 12;
        numberOfStates = 4;
        base.SpawnCandles(singleChandelier, tripleChandelier);
        Instantiate(tripleChandelier, new Vector3(0.37f, 0.807f, -0.106f), Quaternion.Euler(-90f, 90f, 20f));
        Instantiate(tripleChandelier, new Vector3(-0.37f, 0.807f, -0.106f), Quaternion.Euler(-90f, 90f, -20f));

        Instantiate(tripleChandelier, new Vector3(-0.75f, 0.807f, -0.85f), Quaternion.Euler(-90f, 90f, 90f));
        Instantiate(tripleChandelier, new Vector3(0.75f, 0.807f, -0.85f), Quaternion.Euler(-90f, 90f, 90f));
    }
}
