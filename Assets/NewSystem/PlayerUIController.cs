using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour {

    [SerializeField] GameObject handCardObjectPrefab;
    [SerializeField] GameObject cardPanel;

    //List<CardObject> cardsInHand = new List<CardObject>();

    public void ToggleCardPanel(bool toggle)
    {
        cardPanel.SetActive(toggle);
        foreach (Transform child in cardPanel.transform)
        {
            var card = child.GetComponent<CardObject>();
            card.Interactable = toggle;
        }
    }

    public void AddCardToHand(Card card)
    {
        //Create new card object in hand
        var newCardObject = Instantiate(handCardObjectPrefab, cardPanel.transform);
        var newCard = newCardObject.GetComponent<CardObject>();

        //Add object to hand and setup card proprties
        newCard.Card = card;
        //cardsInHand.Add(newCard);
    }
}
