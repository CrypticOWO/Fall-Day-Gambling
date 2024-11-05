using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongSwap : MonoBehaviour
{
    public GhostAudio MusicPlaying;
    public Button TargetButton;
    public TMP_Text Unlocker;

    public void OnButtonClick()
    {
        if (Unlocker.text == "Unlocked")
        {
            // If unlocked, change the song based on the button's name
            string songName = TargetButton.gameObject.name;
            MusicPlaying.ChangeSong(songName);
        }
        else
        {
            float unlockPrice;
            if (float.TryParse(Unlocker.text, out unlockPrice))
            {
                if (Player.Balance >= unlockPrice)
                {
                    // Deduct the unlock price from the player's balance
                    Player.Balance -= unlockPrice;
                    Unlocker.text = "Unlocked";
                }
                else
                {
                    // Not enough balance
                    Debug.Log("Not enough money to unlock this song.");
                }
            }
        }
    }
}
