using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropableArena : MonoBehaviour, IDropHandler{

    public int HERO_ARENA_ID;

    public bool IsArenaFull {
        get {
            return transform.childCount >= maxCardCount;
        }
    }
    int maxCardCount = 1;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableCard dc = eventData.pointerDrag.GetComponent<DraggableCard>();
        DraggableCard.IsDragging = false;

        if (!dc.CanBeMoved) return;
        if (dc != null && dc.HERO_CARD_ID == HERO_ARENA_ID && TurnSystem.Instance.IsHeroTurn(dc.HERO_CARD_ID))
        {
            if(transform.childCount < maxCardCount) { 
                dc.ParentToReturn = this.transform;
                dc.transform.localScale = new Vector2(0.7f, 0.7f);
                dc.IsPlaced = true;
                dc.CardPlaced();
            }
        }
    }
}
