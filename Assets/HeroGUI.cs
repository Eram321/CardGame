using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

public class HeroGUI : MonoBehaviour {

    [SerializeField] GameObject handParent;
    [SerializeField] GameObject cardPrefab;

    HeroController controller;

    bool done = false;

    void Start()
    {
        DraggableCard.cardPlacedOnBoard += CardPlaced;

        controller = GetComponent<HeroController>();
    }
    void OnDestroy()
    {
        DraggableCard.cardPlacedOnBoard -= CardPlaced;
    }

    public void AddCardToHand(Card card, int HERO_ID)
    {
        var obj = Instantiate(cardPrefab, handParent.transform);
        var cardObject = obj.GetComponent<DraggableCard>();
        cardObject.HERO_CARD_ID = HERO_ID;
        cardObject.Card = card;
    }

    public void CardPlaced(string cardID)
    {
        if (TurnSystem.Instance.IsHeroTurn(controller.HERO_ID)) {
            controller.HeroCardPlaced(cardID);
        }
    }

    public bool AllCardsPlaced()
    {
        var dropAreas = FindObjectsOfType<DropableArena>();
        foreach (var dropArea in dropAreas){
            if (TurnSystem.Instance.IsHeroTurn(dropArea.HERO_ARENA_ID))
                if (!dropArea.IsArenaFull)
                    return false;
        }

        return true;
    }
}
