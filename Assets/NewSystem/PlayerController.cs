using Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnEnable()
    {
        CardObject.OnSelectCard += SelectCard;
    }
    private void OnDestroy()
    {
        CardObject.OnSelectCard -= SelectCard;
    }

    Deck hand = new Deck();
    Deck deck = new Deck();

    //Units dropped on map
    List<Unit> units = new List<Unit>();

    //Player Interface
    PlayerUIController PlayerUI;
    
    //Utils
    float depthDistance = 100f;
    Camera cam;

    //Player Turn properites
    private bool isPlayerTurn;
    public bool IsPlayerTurn
    {
        get {
            return isPlayerTurn;
        }

        set {
            isPlayerTurn = value;
            PlayerUI.ToggleCardPanel(value);
        }
    }

    //Selected Card
    CardObject selectedUnit;

    private void Start()
    {
        cam = Camera.main;
        PlayerUI = GetComponent<PlayerUIController>();

        Initialize();
    }

    public void Initialize()
    {
        //Add cards to deck for tests 2xCard
        var cards = Data.ReadAllCards();
        for (int i = 0; i < cards.Count; i++)
        {
            deck.AddCard(cards[i]);
            deck.AddCard(cards[i]);
        }

        //Add 10 cards from deck to hand
        for (int i = 0; i < 10; i++)
        {
            var card = deck.GetNextCard();
            hand.AddCard(card);
            PlayerUI.AddCardToHand(card);
        }
    }

    private void SelectCard(CardObject card)
    {
        if (!isPlayerTurn) return;

        if (selectedUnit) selectedUnit.DisableCard();

        if (selectedUnit == card)
        {
            GameManager.MapController.DisableInit(this);
            selectedUnit = null;
        }
        else
        {
            GameManager.MapController.EnableInit(this);
            selectedUnit = card;
        }
    }
    public void DiselectCard()
    {
        if (selectedUnit) selectedUnit.DisableCard();

        GameManager.MapController.DisableInit(this);
        selectedUnit = null;
    }

    private void Update(){

        if (!isPlayerTurn) return;

        if (Input.GetMouseButtonDown(0))
            PlaceUnit();
    }

    private void PlaceUnit()
    {
        if (selectedUnit == null) return;

        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, depthDistance);
        if (hit)
        {
            var place = hit.collider.GetComponent<Place>();
            if (place)
            {
                var dropped = place.DropCard(selectedUnit, this);
                if (dropped)
                {
                    //Drop unit
                    units.Add(dropped);
                    GameManager.MapController.DisableInit(this);

                    //Remove card from hand
                    hand.RemoveCardWithID(selectedUnit.Card.ID);
                    Destroy(selectedUnit.gameObject);
                    selectedUnit = null;
                }
            }
        }
    }

    public List<Unit> GetUnits()
    {
        return units;
    }
}

