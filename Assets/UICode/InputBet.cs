using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBet : MonoBehaviour
{
    public string BetString;
    public static float Bet = 0;

    public void ReadStringInput(string value)
    {
        BetString = value;
        Bet = float.Parse(BetString);
    }
}