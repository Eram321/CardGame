using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Gameplay { 
    public class Deck {

        List<Card> cards = new List<Card>();

        public int DeckSize
        {
            get {
                return cards.Count;
            }
        }
        public void AddCard(Card card)
        {
            cards.Add(card);
            Shuffle(cards);
        }
        public void RemoveCardWithID(int id)
        {
            for (int i = 0; i < cards.Count; i++){
                if(cards[i].ID == id){
                    cards.RemoveAt(i);
                    break;
                }
            }
        }
        public Card GetNextCard()
        {
            if (cards.Count == 0) return new Card(); // no more cards end game

            var card = cards.First();
            cards.RemoveAt(0);

            return card;
        }

        //Shuffle list
        private System.Random rng = new System.Random();
        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
