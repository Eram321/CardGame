using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image cardImage;

    private Card card;
    public Card Card
    {
        get { return card; }
        set {
            card = value;
            //update interface depends on card
            cardImage.sprite = Resources.Load<Sprite>("Cards/" + card.ImageName + "_card");
        }
    }

    public bool Interactable;

    public delegate void SelectCard(CardObject card);
    public static event SelectCard OnSelectCard;

    public void OnPointerClick(PointerEventData eventData){

        if (Interactable)
        {
            EnableCard();
            OnSelectCard(this);
        }
    }
    public void DisableCard()
    {
        GetComponent<Image>().color = Color.gray;
        GameManager.GameUI.DisableCardPreview();
    }
    public void EnableCard()
    {
        GetComponent<Image>().color = Color.green;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Interactable)
        {
            GameManager.GameUI.SetCardPreview(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Interactable)
        {
            GameManager.GameUI.DisableCardPreview();
        }
    }
}
