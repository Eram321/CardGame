using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI { 
    public class Deck {

        public string Name;
        public CardType Type;
        public string[] CardsID;

        public Deck(string name, CardType type, string[] cardsID)
        {
            Name = name;
            Type = type;
            CardsID = new string[cardsID.Length];
            Array.Copy(cardsID, CardsID, cardsID.Length);
        }
    }
}
