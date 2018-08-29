using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] Text enableTurnsText;

    public Image cardImage;

    private Card card;
    public Card Card
    {
        get { return card; }
        set {
            card = value;
            //update interface depends on card
            if (cardImage) cardImage.sprite = Resources.Load<Sprite>(string.Format("Cards/{0}/{1}_card", card.ImageName, card.ImageName));
            EnableTurns = card.Turns;
            
        }
    }

    public bool Interactable;

    public delegate void SelectCard(CardObject card);
    public static event SelectCard OnSelectCard;

    public void DecTurns(){

        if (enable) return;
        EnableTurns--;

    }
    int enableTurns;
    public int EnableTurns
    {
        get { return enableTurns; }
        set {
            enableTurns = value;
            if (enableTurnsText) enableTurnsText.text = enableTurns.ToString();
            if (enableTurns == 0)
            {
                enable = true;
                if (enableTurnsText) enableTurnsText.gameObject.SetActive(false);
            }
        }
    }
    bool enable = false;

    public void OnPointerClick(PointerEventData eventData){

        if (Interactable && enable)
        {
            EnableCard();
            OnSelectCard(this);
        }
    }
    public void DisableCard()
    {
        var image = GetComponent<Image>();
        if (image) {
            image.color = new Color(0, 0, 0, 0);
            GameManager.GameUI.DisableCardPreview();
        }
    }
    public void EnableCard()
    {
        var image = GetComponent<Image>();
        if (image)
            GetComponent<Image>().color = new Color(0.45f, 0.53f, 0.09f, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.GameUI.SetCardPreview(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.GameUI.DisableCardPreview();
    }
}
