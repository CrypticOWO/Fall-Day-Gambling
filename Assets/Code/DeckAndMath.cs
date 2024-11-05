using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckAndMath : MonoBehaviour
{
    public static int DealerHandValue = 0;
    public static int SecretDealerHandValue = 0;

    public static int PlayerHandValue = 0;

    public static int PlayerDrawnCards = 0;
    public static int DealerDrawnCards = 0;

    public static float Bet = 0;
    public static float Payout = 0;

    public static int Amount = 0;

    [SerializeField] private Texture[] frontTextures; // Array of card textures
    public static Texture[] FrontTextures { get; private set; }

    void Start()
    {
        PlayerDrawnCards = 0;
        DealerDrawnCards = 0;
        Amount = 0;
    }

    void Update()
    {
        AdjustPlayerAceValuesIfNeeded();
        AdjustDealerAceValuesIfNeeded();
        
        if (DealerCode.PlayerNatural == "YEAH" && DealerCode.DealerNatural != "YEAH" && DealerCode.Gameover == "Yes" && Amount == 0)
        {
            Player.Balance += 1.5f*Bet;
            Payout = 1.5f*Bet;
            Amount += 1;
            DealerCode.PlayerNatural = "Nope";
            Debug.Log("Player Natural Win!");
        }
        else if ((PlayerHandValue == DealerHandValue || (DealerCode.PlayerNatural == "YEAH" && DealerCode.DealerNatural == "YEAH")) && DealerCode.Gameover == "Yes" && Amount == 0)
        {
            Player.Balance += Bet;
            Payout = Bet;
            Amount += 1;
            if(DealerCode.PlayerNatural == "YEAH" && DealerCode.DealerNatural == "YEAH")
            {
                DealerCode.PlayerNatural = "Nope";
                DealerCode.DealerNatural = "Nope";
                Debug.Log("Natural Push!");
            }
        }
        else if ((DealerHandValue > 21 || (PlayerHandValue <= 21 && PlayerHandValue > DealerHandValue)) && DealerCode.Gameover == "Yes" && Amount == 0)
        {
            Player.Balance += 2*Bet;
            Payout = 2*Bet;
            Amount += 1;
        }
        else if (DealerHandValue <= 21 && (PlayerHandValue < DealerHandValue || PlayerHandValue > 21) && DealerCode.Gameover == "Yes" && Amount == 0)
        {
            Payout = 0;
            Amount += 1;
            if(DealerCode.DealerNatural == "YEAH")
            {
                DealerCode.DealerNatural = "Nope";
                Debug.Log("Dealer Natural Win!");
            }
        }
    }

    void Awake()
    {
        FrontTextures = frontTextures;
    }

    public static Texture GetRandomTexture()
    {
        int randomIndex = Random.Range(0, FrontTextures.Length);
        return FrontTextures[randomIndex];
    }
    
    private void AdjustPlayerAceValuesIfNeeded()
    {
        while (DeckAndMath.PlayerHandValue > 21 && DealerCode.PlayerAces.Count > 0)
        {
            // Get the last Ace added to the player's hand
            Card aceToAdjust = DealerCode.PlayerAces[DealerCode.PlayerAces.Count - 1];
            
            // Check if the Ace is valued as 11
            if (aceToAdjust.myValue == 11)
            {
                // Change its value to 1
                aceToAdjust.myValue = 1;
                DeckAndMath.PlayerHandValue -= 10; // Decrease hand value by 10

                // Remove the Ace from the list of Aces
                DealerCode.PlayerAces.RemoveAt(DealerCode.PlayerAces.Count - 1);
            }
        }
    }
    
    private void AdjustDealerAceValuesIfNeeded()
    {
        while (DeckAndMath.DealerHandValue > 21 && DealerCode.DealerAces.Count > 0)
        {
            DealerCard aceToAdjust = DealerCode.DealerAces[DealerCode.DealerAces.Count - 1];
            
            // Check if the Ace is valued as 11
            if (aceToAdjust.myValue == 11)
            {
                // Change its value to 1
                aceToAdjust.myValue = 1;
                DeckAndMath.DealerHandValue -= 10;
                
                // Remove the ace from the list
                DealerCode.DealerAces.RemoveAt(DealerCode.DealerAces.Count - 1);
            }
        }
    }
}
