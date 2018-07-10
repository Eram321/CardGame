using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Core;
using System;

public class DraggableCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [Header("UI Reference")]
    [SerializeField] Text apText;
    [SerializeField] Text dfText;
    [SerializeField] Image cardImage;

    Card card;
    public Card Card
    {
        get { return card; }
        set {
            card = value;
            apText.text = card.AttackPoints.ToString();
            dfText.text = card.DefensePoints.ToString();
            cardImage.sprite = Resources.Load<Sprite>("Cards/" + card.ImageName);
        }
    }

    public static bool IsDragging;

    //Drag, Enter, Exit, Placed card 
    public int HERO_CARD_ID;
    public Transform ParentToReturn;
    int positionIndex;
    public bool IsPlaced; // Is card placed on board
    public bool CanBeMoved; //Is card part of current player deck
    public delegate void CardPlacedOnBoard(int cardID);
    public static event CardPlacedOnBoard cardPlacedOnBoard;

    CardPreview cardPreview;
    float offset = 25;
    Vector2 startPos;
    void Start() {
        cardPreview = FindObjectOfType<CardPreview>();
        StartCoroutine(SetStartPositionAfterLayout());
    }

    private IEnumerator SetStartPositionAfterLayout()
    {
        yield return new WaitForSeconds(0.1f);
        startPos = transform.localPosition;
    }

    #region Draggable
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsCardActive()) return;

        startPos = transform.localPosition;
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + offset);
        cardPreview.ChangeCardPreview(cardImage.sprite, card);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsCardActive()) return;

        transform.localPosition = startPos;
        cardPreview.ToggleCardPreview(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsCardActive()) {
            CanBeMoved = false;
            return;
        }

        CanBeMoved = true;
        IsDragging = true;
        positionIndex = transform.GetSiblingIndex();
        ParentToReturn = transform.parent;

        transform.SetParent(transform.root);
        transform.localScale = new Vector2(0.75f, 0.75f);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!CanBeMoved) return;

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!CanBeMoved) return;

        transform.SetParent(ParentToReturn);
        transform.SetSiblingIndex(positionIndex);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        IsDragging = false;
    }
    #endregion

    private bool IsCardActive()
    {
        return TurnSystem.Instance.IsHeroTurn(HERO_CARD_ID)
            && !IsPlaced
            && !IsDragging
            && TurnSystem.Instance.currentHero.IsHeroActionEnabled;
    }

    public void CardPlaced() {
        cardPlacedOnBoard(Card.ID);
        var rect = transform.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        StartCoroutine(ResetCardPosition());
    }

    IEnumerator ResetCardPosition()
    {
        yield return new WaitForSeconds(0.01f);
        transform.localPosition = Vector3.zero;
    }

    public void TakeDamage(int damage)
    {
        card.DefensePoints -= damage;
        if (card.DefensePoints <= 0) Destroy(this.gameObject);
        dfText.text = card.DefensePoints.ToString();
    }

    public IEnumerator StartAttack(DraggableCard opponentCard)
    {
        //set sorting order
        transform.parent.parent.SetAsLastSibling();

        // calculate distance to opponent card
        var dist = Vector3.Distance(transform.position, opponentCard.transform.position);

        var accel = 400;
        while (dist > 80)
        {
            transform.position = Vector3.MoveTowards(transform.position, opponentCard.transform.position, Time.deltaTime * accel);
            dist = Vector3.Distance(transform.position, opponentCard.transform.position);
            yield return new WaitForEndOfFrame();
        }

        var damage = Card.AttackPoints;
        if (Card.VsINF && opponentCard.card.Type == CardType.Infantry
           || Card.VsArch && opponentCard.card.Type == CardType.Archers
           || Card.VsCav && opponentCard.card.Type == CardType.Cavalary)
            damage += (int)(damage * 0.5);

        opponentCard.TakeDamage(damage);
        transform.localPosition = Vector3.zero;
    }

    public IEnumerator HeroAttack(HeroController heroController)
    {
        //set sorting order
        transform.parent.parent.SetAsLastSibling();

        var dist = Vector3.Distance(transform.position, heroController.transform.position);

        var accel = 400;
        while (dist > 80)
        {
            var destPosition = new Vector3(transform.position.x, heroController.transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, destPosition, Time.deltaTime * accel);
            dist = Vector3.Distance(transform.position, destPosition);
            yield return new WaitForEndOfFrame();
        }

        heroController.TakeDamage(Card.AttackPoints);
        transform.localPosition = Vector3.zero;
    }
}
