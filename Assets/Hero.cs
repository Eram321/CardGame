using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;
using System;
using Game.Core;

public class Hero : MonoBehaviour {

    [SerializeField] float heroMaxHealth;
    float currentHeroHealth;

    Deck hand = new Deck();
    Deck deck = new Deck();

    public delegate void HeroTurnEnd();
    public static event HeroTurnEnd onHeroEndTurn;
  
    HeroController controller;

    public float HealthInPercentage
    {
        get {
            return currentHeroHealth/heroMaxHealth;
        }
    }

    public void Initialize()
    {
        controller = GetComponent<HeroController>();
        currentHeroHealth = heroMaxHealth;

        //Add cards to deck for tests 2xCard
        var cards = Data.ReadAllCards();
        for (int i = 0; i < cards.Count; i++) { 
            deck.AddCard(cards[i]);
            deck.AddCard(cards[i]);
        }

        //Add 15 cards from deck to hand
        for (int i = 0; i < 15; i++)
        {
            var card = deck.GetNextCard();
            hand.AddCard(card);
            controller.UpdateHeroHand(card);
        }
    }

    public void RemoveCardFromHand(int cardID)
    {
        hand.RemoveCardWithID(cardID);
        TurnEnd();
    }

    public void TurnEnd()
    {
        onHeroEndTurn();
    }

    public void TakeDamage(float damage)
    {
        currentHeroHealth -= damage;
        //if 0 dieeee
    }

}
