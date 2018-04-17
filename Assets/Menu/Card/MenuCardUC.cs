using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuCardUC : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

    [SerializeField] Image cardImage;
    [SerializeField] Text cardName;
    [SerializeField] Text cardAPText;
    [SerializeField] Text cardDFText;

    [SerializeField] bool Interactive = true;

    Image backgroundImage;
    public delegate void OnMouseEnter(Card card, Image cardImage);
    public static event OnMouseEnter onMouseEnter;

    public delegate void OnMouseClick(string cardID);
    public static event OnMouseClick onMouseClick;

    Card card;
    public Card ItemCard{
        get { return card; }
        set {
            card = value;
            SetCardPreview(card, Resources.Load<Sprite>("Cards/" + card.ImageName));
        }
    }

    void Start(){
        backgroundImage = GetComponent<Image>();
    }

    public void SetCardPreview(Card card, Sprite sprite)
    {
        cardImage.sprite = sprite;
        cardName.text = card.Name;
        cardAPText.text = card.AttackPoints.ToString();
        cardDFText.text = card.DefensePoints.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Interactive) return;

        onMouseEnter(card, cardImage);
        backgroundImage.color = Color.yellow;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Interactive) return;

        backgroundImage.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Interactive) return;

        onMouseClick(card.ID);
    }
}
