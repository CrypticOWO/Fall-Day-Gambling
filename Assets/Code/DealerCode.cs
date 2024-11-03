using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerCode : MonoBehaviour
{
    public GameObject CardPrefab;
    public GameObject DealerCardPrefab;
    public GameObject CardBackPrefab;
    
    public Vector3 lastPlayerCardPosition;
    public Vector3 lastDealerCardPosition;

    public Vector3 CardBackPosition;
    public GameObject CardBack;

    public static string DealerTurn = "Start";
    public static string ButtonsDisabled = "No";

    public static List<GameObject> PlayerHand = new List<GameObject>();
    public static List<GameObject> DealerHand = new List<GameObject>();

    public static List<Card> PlayerAces = new List<Card>();
    public static List<DealerCard> DealerAces = new List<DealerCard>();

    public Button HitButton;
    public Button StandButton;

    public static string PlayerNatural = "Nope";
    public static string DealerNatural = "Nope";

    public static string Gameover = "NA";

    void Start()
    {
        PlayerNatural = "Nope";
        DealerNatural = "Nope";
        DealerTurn = "Start";
        Gameover = "NA";
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraCode.LookingAt == "Table" && CameraCode.LockView == "Yes" && DealerTurn == "Start" && ConfirmBet.BetConfirmed == "Yes")
        {
            SetUpGame();
            DealerTurn = "Standby";
            Gameover = "No";
        }
        if (Gameover == "No")
        {
            if (DealerTurn == "Active" && ButtonsDisabled == "Yes")
            {
                Destroy(CardBack);

                if (DeckAndMath.PlayerHandValue == 21 && DeckAndMath.PlayerDrawnCards == 2)
                {
                    Gameover = "Yes";
                    ConfirmBet.BetConfirmed = "No";
                    DealerTurn = "Standby";
                    PlayerNatural = "YEAH";
                }
                else if (DeckAndMath.PlayerHandValue > 21 || DeckAndMath.DealerHandValue >= 17 || DeckAndMath.PlayerHandValue <= DeckAndMath.DealerHandValue)
                {
                    Gameover = "Yes";
                    ConfirmBet.BetConfirmed = "No";
                    DealerTurn = "Standby";
                }
                else if ((DeckAndMath.DealerHandValue < DeckAndMath.PlayerHandValue) && DeckAndMath.DealerHandValue < 17)
                {
                    DrawDealerCard();
                }
                
            }
            else if (DealerTurn == "Standby" && (DeckAndMath.PlayerHandValue >= 21 || DeckAndMath.DealerHandValue == 21))
            {
                HitButton.interactable = false;
                StandButton.interactable = false;
                DealerCode.DealerTurn = "Active";
                DealerCode.ButtonsDisabled = "Yes";
                if (DeckAndMath.DealerHandValue == 21 && DeckAndMath.DealerDrawnCards == 2)
                {
                    DealerNatural = "YEAH";
                }
            }
        }
    }

    public void SetUpGame()
    {
        Vector3 position;
        DeckAndMath.PlayerDrawnCards = 0;
        DeckAndMath.DealerDrawnCards = 0;

        // Instantiate FIRST Face Up Player Card
        position = new Vector3(0, 2.8f, 19);
        GameObject newCard = Instantiate(CardPrefab, position, Quaternion.identity);
        PlayerHand.Add(newCard);
        newCard.transform.localEulerAngles = new Vector3(0, 90, 90); // Set rotation
        ++DeckAndMath.PlayerDrawnCards;

        // Instantiate FIRST Face Down Dealer Card Part 1
        CardBackPosition = new Vector3(0.6f, 2.801f, 21.5f);
        CardBack = Instantiate(CardBackPrefab, CardBackPosition, Quaternion.identity);
        CardBack.transform.localEulerAngles = new Vector3(0, 90, 90); // Set rotation

        // Instantiate FIRST Face Down Dealer Card Part 2
        position = new Vector3(0.6f, 2.8f, 21.5f);
        GameObject newCard2 = Instantiate(DealerCardPrefab, position, Quaternion.identity);
        DealerHand.Add(newCard2);
        newCard2.transform.localEulerAngles = new Vector3(0, 90, 90); // Set rotation
        ++DeckAndMath.DealerDrawnCards;

        // Instantiate SECOND Face Up Player Card
        position = new Vector3(0.25f, 2.801f, 19.25f);
        GameObject newCard3 = Instantiate(CardPrefab, position, Quaternion.identity);
        PlayerHand.Add(newCard3);
        newCard3.transform.localEulerAngles = new Vector3(0, 90, 90); // Set rotation
        ++DeckAndMath.PlayerDrawnCards;

        // Instantiate SECOND Face Up Dealer Card
        position = new Vector3(-0.05f, 2.802f, 21.5f);
        GameObject newCard4 = Instantiate(DealerCardPrefab, position, Quaternion.identity);
        DealerHand.Add(newCard4);
        newCard4.transform.localEulerAngles = new Vector3(0, 90, 90); // Set rotation
        ++DeckAndMath.DealerDrawnCards;
    }

    public void DrawDealerCard()
    {
        Vector3 position;

        if (DeckAndMath.DealerDrawnCards == 2)
        {
            // Use the specified starting position for the first card
            position = new Vector3(-0.70f, 2.803f, 21.5f);
        }
        else
        {
            // Offset the position based on the last card's position
            position = lastDealerCardPosition + new Vector3(-0.65f, 0.001f, 0);
        }

        // Instantiate the new card
        GameObject newCard = Instantiate(DealerCardPrefab, position, Quaternion.identity);
        newCard.transform.localEulerAngles = new Vector3(0, 90, 90); // Set rotation
        DealerCode.DealerHand.Add(newCard);

        // Update the lastPlayerCardPosition to the position of the newly created card
        lastDealerCardPosition = position;
        ++DeckAndMath.DealerDrawnCards;
    }
}