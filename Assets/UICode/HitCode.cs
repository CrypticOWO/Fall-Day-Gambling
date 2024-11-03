using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCode : MonoBehaviour
{
    public GameObject CardPrefab;
    public Vector3 lastPlayerCardPosition; // Track the last card's position

    public void OnButtonClick()
    {
        DrawPlayerCard();
    }

    public void DrawPlayerCard()
    {
        Vector3 position;

        if (DeckAndMath.PlayerDrawnCards == 2)
        {
            // Use the specified starting position for the first card
            position = new Vector3(0.5f, 2.802f, 19.5f);
        }
        else
        {
            // Offset the position based on the last card's position
            position = lastPlayerCardPosition + new Vector3(0.25f, 0.001f, 0.25f);
        }

        // Instantiate the new card
        GameObject newCard = Instantiate(CardPrefab, position, Quaternion.identity);
        newCard.transform.localEulerAngles = new Vector3(0, 90, 90); // Set rotation
        DealerCode.PlayerHand.Add(newCard);

        // Update the lastPlayerCardPosition to the position of the newly created card
        lastPlayerCardPosition = position;
        ++DeckAndMath.PlayerDrawnCards;
    }
}