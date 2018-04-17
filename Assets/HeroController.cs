using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using Game.Core;

public class HeroController : MonoBehaviour {

    public int HERO_ID;

    HeroGUI GUI;
    Hero HERO;

    //TODO Think about hero as monobehavior 
    void Start()
    {
        GUI = GetComponent<HeroGUI>();
        HERO = GetComponent<Hero>();

        HERO.Initialize();
    }

    public void HeroCardPlaced(string cardID)
    {
        HERO.RemoveCardFromHand(cardID);
    }

    public void UpdateHeroHand(Card card)
    {
        GUI.AddCardToHand(card, HERO_ID);
    }

    public void StartNewTurn()
    {
        //Check for empty slots on board
        if (GUI.AllCardsPlaced()) HERO.TurnEnd();

    }

}
