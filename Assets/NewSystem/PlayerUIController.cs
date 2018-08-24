using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {

    [SerializeField] GameObject handCardObjectPrefab;
    [SerializeField] GameObject cardPanel;
    [SerializeField] GameObject nextCardPanel;
    [SerializeField] GameObject usedCardsPanel;
    [SerializeField] Text deckLeftCards;

    List<CardObject> cardsInHand = new List<CardObject>();

    public void ToggleCardPanel(bool toggle)
    {
        if (cardPanel == null) return;

        foreach (Transform child in cardPanel.transform)
        {
            if(child.childCount > 0) { 
                var card = child.GetChild(0).GetComponent<CardObject>();
                card.Interactable = toggle;
            }
        }
    }

    public IEnumerator AddCardToHand(Card card)
    {
        //Create new card object in next card panel
        if (nextCardPanel != null) {

            bool addOneMoreCard = false;
            CardObject newCard = null;
            //Add object to hand and setup card proprties
            if (nextCardPanel.transform.childCount == 0)
            {
                var newCardObject = Instantiate(handCardObjectPrefab, nextCardPanel.transform);
                newCard = newCardObject.GetComponent<CardObject>();
                newCard.Card = card;
            }
            else
            {
                newCard = nextCardPanel.transform.GetChild(0).GetComponent<CardObject>();
                addOneMoreCard = true;
            }

            newCard.Interactable = false;
            yield return new WaitForSeconds(0.5f);

            //Get next free slot 
            Transform slot = null;
            foreach (Transform child in cardPanel.transform)
            {
                if (child.childCount == 0) { 
                    slot = child;
                    break;
                }
            }

            if (slot) {
                newCard.transform.SetParent(slot);
                newCard.transform.position = slot.transform.position;
                if (!cardsInHand.Contains(newCard)){
                    cardsInHand.Add(newCard);
                }
            }

            yield return new WaitForSeconds(0.5f);
            if (addOneMoreCard)
            {
                var newCardObject = Instantiate(handCardObjectPrefab, nextCardPanel.transform);
                newCard = newCardObject.GetComponent<CardObject>();
                newCard.Card = card;
            }
        }
    }

    public void DeckLeftCount(int value)
    {
       if(deckLeftCards) deckLeftCards.text = value.ToString();
    }

    public void RemoveCard(CardObject co)
    {
        if(cardsInHand.Contains(co))
            cardsInHand.Remove(co);
    }

    public void DecCardTurns()
    {
        foreach (var card in cardsInHand){
            card.DecTurns();
        }
    }

    public IEnumerator AddLastCard(Card card)
    {
        if(nextCardPanel != null)
        if (nextCardPanel.transform.childCount > 0)
        {

            var newCard = nextCardPanel.transform.GetChild(0).GetComponent<CardObject>();
            yield return new WaitForSeconds(0.5f);

            Transform slot = null;
            foreach (Transform child in cardPanel.transform)
            {
                if (child.childCount == 0)
                {
                    slot = child;
                    break;
                }
            }

            if (slot)
            {
                newCard.transform.SetParent(slot);
                newCard.transform.position = slot.transform.position;
                if (!cardsInHand.Contains(newCard))
                    cardsInHand.Add(newCard);
                }
        }
    }
}
