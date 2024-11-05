using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAudio : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip[] songs;       // List of songs (AudioClips)

    // This function changes the song based on the song name
    public void ChangeSong(string songName)
    {
        // Search for the song by its name
        foreach (AudioClip song in songs)
        {
            if (song.name == songName)
            {
                audioSource.clip = song;
                audioSource.Play();  // Play the new song
                return;  // Exit once the song is found and changed
            }
        }
        Debug.LogWarning("Song not found: " + songName);
    }
}