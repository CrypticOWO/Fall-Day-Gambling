using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerCode : MonoBehaviour
{
    public GameObject PlayerCardPrefab;
    public GameObject DealerCardPrefab;

    public Vector3 lastPlayerCardPosition;
    public Vector3 lastDealerCardPosition;

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

    public static Vector3 DeckPosition = new Vector3(0, 0, 0); // Set the deck/starting position
    public static Vector3 DealerTargetPosition = new Vector3(0, 0, 0); // Set the dealer target position
    public static Vector3 PlayerTargetPosition = new Vector3(0, 0, 0); // Set the player target position

    private Quaternion TargetRotation = Quaternion.Euler(0, 0, 0); // Set the target rotation for face up

    public static string ScoreUpdate = "No";

    void Start()
    {
        PlayerNatural = "Nope";
        DealerNatural = "Nope";
        DealerTurn = "Start";
        Gameover = "NA";
        ScoreUpdate = "No";
    }

    void Update()
    {
        if (CameraCode.LookingAt == "Table" && CameraCode.LockView == "Yes" && DealerTurn == "Start" && ConfirmBet.BetConfirmed == "Yes")
        {
            SetUpGame();
            DealerTurn = "Standby";
            Gameover = "No";
        }
        
        if (DealerTurn == "Active" && ButtonsDisabled == "Yes" && Gameover == "No")
        {
            StartCoroutine(RevealFaceDown());

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
            else if (DeckAndMath.DealerHandValue < 17)
            {
                StartCoroutine(DrawDealerCard());
            }
        }
        else if (DealerTurn == "Standby" && (DeckAndMath.PlayerHandValue >= 21 || DeckAndMath.DealerHandValue == 21))
        {
            HitButton.interactable = false;
            StandButton.interactable = false;
            DealerTurn = "Active";
            ButtonsDisabled = "Yes";
            if (DeckAndMath.DealerHandValue == 21 && DeckAndMath.DealerDrawnCards == 2)
            {
                DealerNatural = "YEAH";
            }
        }
    }

    public void SetUpGame()
    {
        DeckPosition = new Vector3(10f, 3f, 76.5f);
        DealerTargetPosition = new Vector3(8.5f, 2.9f, 76f);
        PlayerTargetPosition = new Vector3(8f, 2.9f, 73.75f);
        TargetRotation = Quaternion.Euler(180, 90, 90);

        DeckAndMath.PlayerDrawnCards = 0;
        DeckAndMath.DealerDrawnCards = 0;

        HitButton.interactable = false;
        StandButton.interactable = false;

        // Start the card drawing sequence as a coroutine
        StartCoroutine(CardDrawingSequence());
    }

    public IEnumerator DrawDealerCard()
    {
        // Instantiate the new card at the starting position (face down)
        DealerCode.ScoreUpdate = "No";
        GameObject newCard = Instantiate(DealerCardPrefab, DeckPosition, Quaternion.Euler(0, 90, 90));
        DealerHand.Add(newCard);
        ++DeckAndMath.DealerDrawnCards;

        // Start the movement and flipping coroutine
        yield return StartCoroutine(MoveAndFlipCard(newCard, DealerTargetPosition + new Vector3((DeckAndMath.DealerDrawnCards - 1) * (-0.65f), 0, 0), TargetRotation, 0.35f)); // 1f is the duration for the movement and flip
        ScoreUpdate = "Yes";
    }
    
    public IEnumerator DrawPlayerCard()
    {
        // Instantiate the new card at a fixed starting position (face down)
        DealerCode.ScoreUpdate = "No";
        GameObject newCard = Instantiate(PlayerCardPrefab, DeckPosition, Quaternion.Euler(0, 90, 90));
        DealerCode.PlayerHand.Add(newCard);
        ++DeckAndMath.PlayerDrawnCards;

        // Start the movement and flipping coroutine
        yield return StartCoroutine(MoveAndFlipCard(newCard, PlayerTargetPosition + new Vector3((DeckAndMath.PlayerDrawnCards - 1) * (0.25f), (DeckAndMath.PlayerDrawnCards) * (0.002f), (DeckAndMath.PlayerDrawnCards) * (0.25f)), TargetRotation, 0.35f)); // 1f is the duration for the movement and flip
        ScoreUpdate = "Yes";
    }

    public IEnumerator RevealFaceDown()
    {
        DealerCode.ScoreUpdate = "No";
        GameObject CardToFlip = DealerHand[0];
        yield return StartCoroutine(MoveAndFlipCard(CardToFlip, new Vector3(8.5f, 2.9f, 76f), Quaternion.Euler(180, 90, 90), 0.70f));
        ScoreUpdate = "Yes";
    }

    private IEnumerator MoveAndFlipCard(GameObject card, Vector3 endPosition, Quaternion endRotation, float duration)
    {
        Vector3 startPosition = card.transform.position;
        Quaternion startRotation = card.transform.rotation;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            // Interpolate position
            card.transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
            
            // Interpolate rotation (flip the card)
            card.transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / duration);

            yield return null; // Wait for the next frame
        }
    }

    private IEnumerator CardDrawingSequence()
    {
        // Instantiate FIRST Face Up Player Card
        yield return DrawPlayerCard();
        yield return new WaitForSeconds(0.35f); // Wait for the card to finish animating

        // Instantiate FIRST Face Down Dealer Card
        TargetRotation = Quaternion.Euler(0, 90, 90);
        yield return DrawDealerCard();
        yield return new WaitForSeconds(0.35f); // Wait for the card to finish animating

        // Instantiate SECOND Face Up Player Card
        TargetRotation = Quaternion.Euler(180, 90, 90);
        yield return DrawPlayerCard();
        yield return new WaitForSeconds(0.35f); // Wait for the card to finish animating

        // Instantiate SECOND Face Up Dealer Card
        yield return DrawDealerCard();
        yield return new WaitForSeconds(0.35f); // Wait for the card to finish animating

        HitButton.interactable = true;
        StandButton.interactable = true;
    }
}
