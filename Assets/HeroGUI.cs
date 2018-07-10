using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using UnityEngine.UI;

public class HeroGUI : MonoBehaviour {

    public List<DropableArena> dropableAreas = new List<DropableArena>();

    [SerializeField] GameObject handParent;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Image healthBar;
    
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

    public void CardPlaced(int cardID)
    {
        if (TurnSystem.Instance.IsHeroTurn(controller.HERO_ID)) {
            controller.HeroCardPlaced(cardID);
        }
    }

    public void ResetCards()
    {
        foreach (Transform child in handParent.transform){
            child.localScale = new Vector2(0.75f, 0.75f);
        }
    }

    public void UpdateHeroHealthBar(float fillAmount)
    {
        healthBar.fillAmount = fillAmount;
    }

    public bool AllCardsPlaced()
    {
        foreach (var dropArea in dropableAreas) {

                if (!dropArea.IsArenaFull)
                    return false;
        }

        return true;
    }
}
