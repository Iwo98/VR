using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    protected int numberOfCandles;
    public int[] candleStatesOriginal;
    public int[] candleStates;
    protected int numberOfStates;
    protected CandleController[] candleControllers;

    public virtual void SpawnCandles(GameObject singleChandelier, GameObject tripleChandelier)
    {
        candleStatesOriginal = new int[numberOfCandles];
        candleStates = new int[numberOfCandles];
    }

    public void RandomizeCandles(int[] results)
    {
        candleControllers = FindObjectsOfType<CandleController>();

        for (int i = 0; i < numberOfCandles; i++)
        {
            results[i] = Random.Range(0, numberOfStates);
        }

        int n = 0;
        foreach (CandleController childCC in candleControllers)
        {
            if (results[n] == 0)
            {
                childCC.SetOff();
            }
            else if (results[n] == 1)
            {
                childCC.SetOnFire();
            }
            else if (results[n] == 2)
            {
                childCC.SetOnIce();
            }
            else if (results[n] == 3)
            {
                childCC.SetOnPoison();
            }
            n++;
        }
    }

    public int GetNumberOfCandles()
    {
        return numberOfCandles;
    }

    public int CheckCandles()
    {
        int counter = 0;
        for (int i = 0; i < numberOfCandles; i++)
        {
            if (candleStatesOriginal[i] == candleControllers[i].GetState())
            {
                counter++;
            }
        }
        return counter;
    }
}
