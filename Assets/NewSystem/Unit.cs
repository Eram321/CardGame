using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    [SerializeField] SpriteRenderer spriteRenderer;

    private Card card;
    public Card Card
    {
        get { return card; }
        set {
            card = value;
            //update sr depends on card
            spriteRenderer.sprite = Resources.Load<Sprite>("Cards/" + card.ImageName + "_unit");
        }
    }

    public bool CalculateDamage(Unit unit)
    {
        card.Defense -= unit.Card.Attack;
        if (card.Defense <= 0) return true;

        return false;
    }
}
