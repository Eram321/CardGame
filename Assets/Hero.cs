using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    public int HERO_ID;

    Deck hand = new Deck();
    Deck deck = new Deck();

    public delegate void HeroTurnEnd();
    public static event HeroTurnEnd onHeroEndTurn;

    public bool active;
    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
        }
    }

    HeroGUI GUI;

    // Use this for initialization
    void Start () {

        GUI = GetComponent<HeroGUI>();

        //[TEST]
        for (int i = 0; i < 30; i++)
        {
            Card c = new Card();
            deck.AddCard(c);
        }

        for (int i = 0; i < 10; i++)
        {
            var card = deck.GetNextCard();
            hand.AddCard(card);
            GUI.AddCardToHand(card, HERO_ID);
        }
        //[TEST END]
    }

    // Update is called once per frame
    void Update () {
		
	}


    public void StartNewTurn()
    {
        onHeroEndTurn();//after placed card
    }
}
