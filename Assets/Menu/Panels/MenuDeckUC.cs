using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MenuDeckUC : MonoBehaviour,IPointerDownHandler {

    [SerializeField] Text deckNameLabel;

    public delegate void OnMouseClick(UI.Deck deck);
    public static event OnMouseClick onMouseClick;

    private UI.Deck deck;
    public UI.Deck ItemDeck
    {
        get { return deck; }
        set {
            deck = value;
    
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onMouseClick(deck);
    }

    public void UpdateDeckName()
    {

    }
}
