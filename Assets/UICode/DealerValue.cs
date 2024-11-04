using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DealerValue : MonoBehaviour
{
    public TMP_Text DealerScore;
    public GameObject SecondDealerCard;

    // Update is called once per frame
    void Update()
    {
        if (DealerCode.ButtonsDisabled == "No" && DealerCode.ScoreUpdate == "Yes")
        {
            // Update the dealer score only if there's enough cards
            if (DealerCode.DealerHand.Count > 1)
            {
                SecondDealerCard = DealerCode.DealerHand[1];
                DealerCard cardScript = SecondDealerCard.GetComponentInChildren<DealerCard>();
                DealerScore.text = "Dealer Value: " + cardScript.myValue + "+";
            }
            else
            {
                DealerScore.text = "Dealer Value: Pending";
                DealerCode.ScoreUpdate = "No";
            }
        }
        if (DealerCode.ButtonsDisabled == "Yes" && DealerCode.Gameover == "Yes" && DealerCode.ScoreUpdate == "Yes")
        {
            DealerScore.text = "Dealer Value: " + DeckAndMath.DealerHandValue;
        }
    }
}