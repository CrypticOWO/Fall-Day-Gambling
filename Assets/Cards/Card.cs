using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardValue { A, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, J, Q, K };
    public CardValue myValue;

    void Start()
    {
        // Get the MeshRenderer component
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Create a material for the front face
        Material frontMaterial = new Material(Shader.Find("Standard"));

        // Pick a random texture from the DeckAndMath
        Texture selectedTexture = DeckAndMath.GetRandomTexture();

        // Check if a texture was retrieved successfully
        if (selectedTexture != null)
        {
            // Assign the selected texture to the material
            frontMaterial.mainTexture = selectedTexture;

            // Set the card value based on the texture name
            myValue = GetValueFromTextureName(selectedTexture.name);
            Debug.Log($"Selected texture: {selectedTexture.name}, Card value set to: {myValue}");

            // Assign the front material to the MeshRenderer
            meshRenderer.material = frontMaterial;
        }
        else
        {
            Debug.LogWarning("Failed to assign texture: No texture found.");
        }
    }

    private CardValue GetValueFromTextureName(string textureName)
    {
        // Get the character before the last one
        char characterBeforeLast = textureName[textureName.Length - 2];

        // Determine card value based on the character before the last one
        switch (characterBeforeLast)
        {
            case 'A': return CardValue.A;
            case '2': return CardValue.Two;
            case '3': return CardValue.Three;
            case '4': return CardValue.Four;
            case '5': return CardValue.Five;
            case '6': return CardValue.Six;
            case '7': return CardValue.Seven;
            case '8': return CardValue.Eight;
            case '9': return CardValue.Nine;
            case '0': return CardValue.Ten;
            case 'J': return CardValue.J;
            case 'Q': return CardValue.Q;
            case 'K': return CardValue.K;
            default:
                Debug.LogWarning($"Character '{characterBeforeLast}' does not correspond to any card value.");
                return CardValue.A; // Default value or handle as needed
        }
    }
}
