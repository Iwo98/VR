using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDisplay : MonoBehaviour
{
    public DigitDisplay CorrectTenDisplay, CorrectSingleDisplay, WrongTenDisplay, WrongSingleDisplay;

    public void UpdateCorrect(int number)
    {
        if (CorrectTenDisplay && CorrectSingleDisplay)
        {
            CorrectTenDisplay.SendMessage("UpdateDigit", (int)(number / 10));
            CorrectSingleDisplay.SendMessage("UpdateDigit", (int)(number % 10));
        }
    }

    public void UpdateErrors(int number)
    {
        if (WrongTenDisplay && WrongSingleDisplay)
        {
            WrongTenDisplay.SendMessage("UpdateDigit", (int)(number / 10));
            WrongSingleDisplay.SendMessage("UpdateDigit", (int)(number % 10));
        }
    }
}
