using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBet : MonoBehaviour
{
    public static string BetConfirmed = "No";

    public Button HitButton;
    public Button StandButton;

    public void OnButtonClick()
    {
        if (InputBet.Bet > 0 && InputBet.Bet <= Player.Balance)
        {
            DeckAndMath.Bet = InputBet.Bet;
            Player.Balance -= DeckAndMath.Bet;
            BetConfirmed = "Yes";

            if (DeckAndMath.Amount == 1)
            {
                // Destroy all GameObjects in PlayerHand
                foreach (GameObject card in DealerCode.PlayerHand)
                {
                    Destroy(card);
                }
                DealerCode.PlayerHand.Clear(); // Clear the list

                // Destroy all GameObjects in DealerHand
                foreach (GameObject card in DealerCode.DealerHand)
                {
                    Destroy(card);
                }
                DealerCode.DealerHand.Clear(); // Clear the list

                DealerCode.PlayerAces.Clear(); // Clear Player Aces
                DealerCode.DealerAces.Clear(); // Clear Dealer Aces
                
                DealerCode.DealerTurn = "Start";
                DealerCode.Gameover = "NA";
                DealerCode.ButtonsDisabled = "No";

                DeckAndMath.DealerHandValue = 0;
                DeckAndMath.SecretDealerHandValue = 0;
                DeckAndMath.PlayerHandValue = 0;

                DeckAndMath.Amount = 0;

                HitButton.interactable = true;
                StandButton.interactable = true;
            }
        }
        else if (InputBet.Bet > Player.Balance)
        {
            Debug.Log("Not Enough Money LOL");
        }
    }
}
