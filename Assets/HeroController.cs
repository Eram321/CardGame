using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class HeroController : MonoBehaviour {

    public int HERO_ID;

    HeroGUI GUI;
    Hero HERO;

    public bool IsHeroActionEnabled;
    
    //TODO Think about hero as monobehavior 
    void Start()
    {
        GUI = GetComponent<HeroGUI>();
        HERO = GetComponent<Hero>();

        HERO.Initialize();
    }

    public void HeroCardPlaced(int cardID)
    {
        HERO.RemoveCardFromHand(cardID);
    }

    public void UpdateHeroHand(Card card)
    {
        GUI.AddCardToHand(card, HERO_ID);
    }

    public void ResetUI(){
        GUI.ResetCards();
    }

    public void StartNewTurn()
    {
        //Check for empty slots on board
        if (GUI.AllCardsPlaced()) HERO.TurnEnd();
    }

    public void TakeDamage(float damage)
    {
        HERO.TakeDamage(damage);
        GUI.UpdateHeroHealthBar(HERO.HealthInPercentage);
    }

    public List<DropableArena> GetDroppableAreas() {
        return GUI.dropableAreas;
    }

}
