using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Core;

public class DraggableCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public int HERO_CARD_ID;

    public Transform ParentToReturn;

    int positionIndex;

    public bool IsPlaced; // Is card placed on board
    public bool CanBeMoved; //Is card part of current player deck

    public Card Card;

    public delegate void CardPlacedOnBoard(string cardID);
    public static event CardPlacedOnBoard cardPlacedOnBoard;

    #region Draggable
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsCardActive()) return;

        transform.localScale = new Vector2(2, 2);
    }
   
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsCardActive()) return;

        transform.localScale = new Vector2(1, 1);

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsCardActive()){
            CanBeMoved = false;
            return;
        }

        CanBeMoved = true;

        positionIndex = transform.GetSiblingIndex();
        ParentToReturn = transform.parent;

        transform.SetParent(transform.root);
        transform.localScale = new Vector2(1, 1);
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
    }
    #endregion

    private bool IsCardActive()
    {
        return TurnSystem.Instance.IsHeroTurn(HERO_CARD_ID) && !IsPlaced;
    }

    public void CardPlaced(){
        cardPlacedOnBoard(Card.ID);
    }
}
