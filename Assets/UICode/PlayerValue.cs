using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerValue : MonoBehaviour
{
    public TMP_Text PlayerScore;

    // Update is called once per frame
    void Update()
    {
        PlayerScore.text = "Player Value: " + DeckAndMath.PlayerHandValue;
    }
}
