using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCode : MonoBehaviour
{
    public GameObject PlayerCardPrefab;
    private Quaternion TargetRotation = Quaternion.Euler(180, 90, 90);

    public void OnButtonClick()
    {
        StartCoroutine(DrawPlayerCard());
    }

    public IEnumerator DrawPlayerCard()
    {
        // Instantiate the new card at a fixed starting position (face down)
        DealerCode.ScoreUpdate = "No";
        GameObject newCard = Instantiate(PlayerCardPrefab, DealerCode.DeckPosition, Quaternion.Euler(0, 90, 90));
        ++DeckAndMath.PlayerDrawnCards; // Update drawn cards count
        DealerCode.PlayerHand.Add(newCard);

        // Start the movement and flipping coroutine
        yield return StartCoroutine(MoveAndFlipCard(newCard, DealerCode.PlayerTargetPosition + new Vector3((DeckAndMath.PlayerDrawnCards - 1) * (0.25f), (DeckAndMath.PlayerDrawnCards) * (0.002f), (DeckAndMath.PlayerDrawnCards) * (0.25f)), TargetRotation, 0.35f)); // 1f is the duration for the movement and flip
        DealerCode.ScoreUpdate = "Yes";
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
}
