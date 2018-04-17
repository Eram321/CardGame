using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropableArena : MonoBehaviour, IDropHandler{

    public int HERO_ARENA_ID;

    public bool IsArenaFull {
        get { return currentCardcount >= maxCardCount; }
    }
    int maxCardCount = 4;
    int currentCardcount;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableCard dc = eventData.pointerDrag.GetComponent<DraggableCard>();
        
        if (!dc.CanBeMoved) return;
        if (dc != null && dc.HERO_CARD_ID == HERO_ARENA_ID && TurnSystem.Instance.IsHeroTurn(dc.HERO_CARD_ID))
        {
            if(currentCardcount < maxCardCount) { 
                currentCardcount++;
                dc.ParentToReturn = this.transform;
                dc.IsPlaced = true;
                dc.CardPlaced();
            }
        }
    }
}
