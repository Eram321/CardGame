using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using System;
using Game.Core;

public class Hero : MonoBehaviour {

    Deck hand = new Deck();
    Deck deck = new Deck();

    public delegate void HeroTurnEnd();
    public static event HeroTurnEnd onHeroEndTurn;
  
    HeroController controller;

    public void Initialize()
    {
        controller = GetComponent<HeroController>();

        //Add cards to deck for tests
        for (int i = 0; i < 30; i++)
        {
            Card c = new Card();
            deck.AddCard(c);
        }
        //add 10 cards from deck to hand
        for (int i = 0; i < 10; i++)
        {
            var card = deck.GetNextCard();
            hand.AddCard(card);
            controller.UpdateHeroHand(card);
        }

    }

    public void RemoveCardFromHand(string cardID)
    {
        hand.RemoveCardWithID(cardID);
        TurnEnd();
    }

    public void TurnEnd()
    {
        onHeroEndTurn();
    }
}
