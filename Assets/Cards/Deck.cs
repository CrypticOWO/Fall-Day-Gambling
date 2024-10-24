using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    
    public static Deck Instance {get;private set;} //Singleton

    [SerializeField] private CardCollection _PlayerDeck; //Reference to the actual deck ie. the cards in the deck == CardCollection
    [SerializeField] private Card _CardPrefab; //prefab makes copies of the cards in deck

    [SerializeField] private Canvas _CardCanvas;

    //Instantiated Cards
    private List<Card> _DeckPile;

    public List<Card> HandCards {get;private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InstantiateDeck(); //Make the Deck
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateDeck()
    {
        Card card = Instantiate(_CardPrefab, _CardCanvas.transform); //Instantiates the card prefab as a child of the card canvas
    }
}
