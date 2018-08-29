using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsPanel : Window {

    [SerializeField] Transform content;
    [SerializeField] GameObject cardPrefab;

    private void Start()
    {
        base.Start();
        CreatePanel();
    }

    public void CreatePanel()
    {
        var deck = Game.Instance.PlayerData.PlayerDeck;

        foreach (var card in deck.Cards)
        {
            InstantiateNewCard(deck.GetCard(card.ID));
        }
    }

    private void InstantiateNewCard(Card card)
    {
        var prevObj = Instantiate(cardPrefab, content);
        var prev = prevObj.GetComponent<CardPreviewMenu>();
        prev.Set(card);
    }

    public void ReplaceCard(CardPreviewMenu preview, Card newCard)
    {
        var index = 0;
        foreach (Transform child in content){
            if(child == preview.transform)
                break;
            index++;
        }

        Destroy(preview.gameObject);

        var newPrev = Instantiate(cardPrefab, content);
        var prev = newPrev.GetComponent<CardPreviewMenu>();
        newPrev.transform.SetSiblingIndex(index);
        prev.Set(newCard);
    }

    public void Recruit()
    {
        var recruitCost = 100;

        if (Game.Instance.PlayerData.Gold >= recruitCost)
        {
            Game.Instance.PlayerData.Gold -= recruitCost;
            Data.AddNewCard();
            Menu.Instance.PlayerDataUI.UpdateGoldText();
            InstantiateNewCard(Data.ReadCardWithID(0));
        }
    }
}
