using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BetAndPayout : MonoBehaviour
{
    public TMP_Text CurrentBetOrPayout;

    // Update is called once per frame
    void Update()
    {
        if (ConfirmBet.BetConfirmed == "Yes")
        {
            CurrentBetOrPayout.text = "Current Bet: " + DeckAndMath.Bet;
        }
        else if (ConfirmBet.BetConfirmed == "No")
        {
            CurrentBetOrPayout.text = "Payout: " + DeckAndMath.Payout;
        }
    }
}