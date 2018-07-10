using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI { 
    public class Deck {

        public string Name;
        public CardType Type;
        public int[] CardsID;

        public Deck(string name, CardType type, int[] cardsID)
        {
            Name = name;
            Type = type;
            CardsID = new int[cardsID.Length];
            Array.Copy(cardsID, CardsID, cardsID.Length);
        }
    }
}
