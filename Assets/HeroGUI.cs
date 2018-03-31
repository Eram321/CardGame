using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGUI : MonoBehaviour {

    [SerializeField] GameObject handParent;
    [SerializeField] GameObject cardPrefab;

    public void AddCardToHand(Card card, int HERO_ID)
    {
        var obj = Instantiate(cardPrefab, handParent.transform);
        var cardObject = obj.GetComponent<CardObject>();
        cardObject.HERO_CARD_ID = HERO_ID;
    }

}
