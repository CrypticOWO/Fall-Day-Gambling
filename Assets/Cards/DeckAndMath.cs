using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckAndMath : MonoBehaviour
{
    public int ComputerScore = 0;
    public int PlayerScore = 0;

    [SerializeField] private Texture[] frontTextures; // Array of card textures
    public static Texture[] FrontTextures { get; private set; }

    void Awake()
    {
        FrontTextures = frontTextures;
    }

    public static Texture GetRandomTexture()
    {
        int randomIndex = Random.Range(0, FrontTextures.Length);
        return FrontTextures[randomIndex];
    }
}
