using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int myValue;
    private Texture selectedTexture;

    void Start()
    {
        // Get the MeshRenderer component
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Create a material for the front face
        Material frontMaterial = new Material(Shader.Find("Standard"));

        // Pick a random texture from the DeckAndMath
        selectedTexture = DeckAndMath.GetRandomTexture();

        // Check if a texture was retrieved successfully
        if (selectedTexture != null)
        {
            // Assign the selected texture to the material
            frontMaterial.mainTexture = selectedTexture;

            // Assign the front material to the MeshRenderer
            meshRenderer.material = frontMaterial;

            myValue = GetValueFromTextureName(selectedTexture.name);
            //Debug.Log($"Selected texture: {selectedTexture.name}, Card value set to: {myValue}");

            //Update PlayerHandValue
            DeckAndMath.PlayerHandValue += myValue;
            //Debug.Log($"Current Player Hand Value = {DeckAndMath.PlayerHandValue}");

            if (myValue == 11)
            {
                DealerCode.PlayerAces.Add(this);
            }
        }
        else
        {
            Debug.LogWarning("Failed to assign texture: No texture found.");
        }
    }

    void Update()
    {
        
    }

    private int GetValueFromTextureName(string textureName)
    {
    // Get the character before the last one
    char characterBeforeLast = textureName[textureName.Length - 2];

    // Determine card value based on the character before the last one
    switch (characterBeforeLast)
        {
        case 'A': return 11;
        case '2': return 2;
        case '3': return 3;
        case '4': return 4;
        case '5': return 5;
        case '6': return 6;
        case '7': return 7;
        case '8': return 8;
        case '9': return 9;
        case '0': return 10;
        case 'J': return 10;
        case 'Q': return 10;
        case 'K': return 10;
        default:
            Debug.LogWarning($"Character '{characterBeforeLast}' does not correspond to any card value.");
            return 68; // Default or error value
        }
    }
}
