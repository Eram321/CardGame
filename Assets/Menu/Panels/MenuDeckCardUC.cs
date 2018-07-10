using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuDeckCardUC : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {

    [SerializeField] Text cardName;
    [SerializeField] Image cardImage;

    public delegate void OnMouseEnter(Card card, Image cardImage);
    public static event OnMouseEnter onMouseEnter;

    public delegate void OnMouseClick();
    public static event OnMouseClick onMouseClick;

    public Card card;

    public void SetCardInfo(int cardID)
    {
        //Set current card
        var cards = Data.ReadAllCards();
        foreach (Card c in cards){
            if(c.ID == cardID){
                cardImage.sprite = Resources.Load<Sprite>("Cards/" + c.ImageName);
                card = c;
                break;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onMouseEnter(card, cardImage);
    }

    public void OnPointerDown(PointerEventData eventData){
        Destroy(this.gameObject);
        onMouseClick(); 
    }
}
