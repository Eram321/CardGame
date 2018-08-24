using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI { 
    public class Deck {

        public int[] CardsID;

        public Deck(int[] cardsID)
        {
            CardsID = new int[cardsID.Length];
            Array.Copy(cardsID, CardsID, cardsID.Length);
        }
    }
}
