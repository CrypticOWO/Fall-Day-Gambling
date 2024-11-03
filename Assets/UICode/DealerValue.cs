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
        if (DealerCode.ButtonsDisabled == "No")
        {
            SecondDealerCard = DealerCode.DealerHand[1];
            DealerCard cardScript = SecondDealerCard.GetComponent<DealerCard>();
            DealerScore.text = "Dealer Value: " + cardScript.myValue + "+";
        }
        else
        {
            DealerScore.text = "Dealer Value: " + DeckAndMath.DealerHandValue;            
        }
    }
}