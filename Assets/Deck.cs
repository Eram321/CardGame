using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Gameplay { 
    public class Deck : MonoBehaviour {

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
        }
        public void RemoveCardWithID(string id)
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
            if (cards.Count == 0) return new Card();

            var card = cards.First();
            cards.RemoveAt(0);

            return card;
        }
    }
}
