using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UI { 
    public class Deck {

        public List<DeckCard> Cards = new List<DeckCard>();

        public Deck(List<DeckCard> cards)
        {
            foreach (var c in cards)
            {
                var newC = new DeckCard(c.ID, c.Experience);
                this.Cards.Add(newC);
            }
        }

        internal float GetExperience(int iD, int v)
        {
            return Cards[v].Experience;
        }

        internal void UpgradeCard(Card card, Card newCard, int index)
        {
            var deckCard = new DeckCard(newCard.ID, Cards[index].Experience);

            Cards.RemoveAt(index);
            Cards.Insert(index, deckCard);
        }

        internal Card GetCard(int iD)
        {
            var cards = Data.ReadAllCards();
            return cards.SingleOrDefault(c => c.ID == iD);
        }

        internal void AddNewCard()
        {
            var card = new DeckCard(0, 0.0f);
            Cards.Add(card);
        }
    }
}

public class DeckCard
{
    public int ID;
    public float Experience;

    public DeckCard(int id, float exp)
    {
        this.ID = id;
        this.Experience = exp;
    }
}
