﻿using Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public delegate void PlayerStartTurn();
    public event PlayerStartTurn OnStartTurn;

    private void OnEnable()
    {
        CardObject.OnSelectCard += SelectCard;
    }
    private void OnDestroy()
    {
        CardObject.OnSelectCard -= SelectCard;
    }

    public Deck hand = new Deck();
    Deck deck = new Deck();

    //Controllers
    UnitsController unitsController;

    //Player Commander
    public Commander commander;
    [SerializeField] int playerDeckNumber = 0;

    //Units dropped on map
    List<Unit> units = new List<Unit>();

    //Player Interface
    PlayerUIController PlayerUI;
    
    //Utils
    float depthDistance = 100f;
    Camera cam;
    public int maxCardsInHand = 5;

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
    public CardObject selectedUnit;

    bool firstRound = true;

    public UI.Deck CommanderCards;

    private void Start()
    {
        cam = Camera.main;
        PlayerUI = GetComponent<PlayerUIController>();
        unitsController = GetComponent<UnitsController>();

        if (playerDeckNumber == 0) CommanderCards = Game.Instance.PlayerData.PlayerDeck;
        else CommanderCards = Data.ReadDeckWithID(Game.Instance.CurrentEnemyDeck);
    }

    public void Initialize()
    {
        StartCoroutine(FillHand());
    }

    private IEnumerator FillHand()
    {
        //Add card to gameplay deck from player deck
        var cards = Data.ReadAllCards();
        foreach (var deckCard in CommanderCards.Cards)
        {
            var card = cards.Single(c => c.ID == deckCard.ID);
            deck.AddCard(card);
        }

        //Fill hand with cards
        for (int i = 0; i <= maxCardsInHand; i++)
        {
            PlayerUI.DeckLeftCount(deck.DeckSize);
            var card = deck.GetNextCard();
            hand.AddCard(card);
            yield return StartCoroutine(PlayerUI.AddCardToHand(card));
        }
        PlayerUI.ToggleCardPanel(true);
        firstRound = false;

    }

    public IEnumerator StartTurn()
    {
        PlayerUI.ToggleCardPanel(false);
        PlayerUI.DecCardTurns();

        if (firstRound)
        {
            Initialize();
        }
        else
        {
            //take card from deck if is not full
            if (hand.DeckSize <= maxCardsInHand) { 

                if(deck.DeckSize > 0) {

                   var card = deck.GetNextCard();
                   hand.AddCard(card);
                   PlayerUI.DeckLeftCount(deck.DeckSize);
                   yield return StartCoroutine(PlayerUI.AddCardToHand(card));
                }
                else
                {
                    Debug.Log("No more cards");
                    yield return StartCoroutine(PlayerUI.AddLastCard(new Card()));
                }
            }
            else
            {
                Debug.Log("hand is full");
                //hand is full
            }

        }

        PlayerUI.ToggleCardPanel(true);

        if (OnStartTurn != null) OnStartTurn();
    }

    private void SelectCard(CardObject card)
    {
        if (!isPlayerTurn) return;

        if (selectedUnit) selectedUnit.DisableCard();

        if (selectedUnit == card)
        {
            //depends on card type
            //if(card == skill)
            //else if(card == unit) 
            unitsController.DisablePlaces(this);
            selectedUnit = null;
        }
        else
        {
            //depends on card type
            //if(card == skill)
            //else if(card == unit) 
            unitsController.EnablePlaces(this);
            selectedUnit = card;
        }
    }
    public void DiselectCard()
    {
        if (selectedUnit) selectedUnit.DisableCard();

        //depends on card type
        //if(card == skill)
        //else if(card == unit) 
        unitsController.DisablePlaces(this);
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
                PlaceAt(place);
            }
        }
    }

    public void PlaceAt(Place place)
    {
        //depends on card type
        //if(card == skill)
        //else if(card == unit) 

        if (unitsController.PlaceCard(place, selectedUnit, this, units))
        {
            //Remove card from hand
            hand.RemoveCardWithID(selectedUnit.Card.ID);
            PlayerUI.RemoveCard(selectedUnit);
            Destroy(selectedUnit.gameObject);
            selectedUnit = null;
        }
    }

    public List<Unit> GetUnits()
    {
        return units;
    }

    public void AttackCommander(float damage)
    {
        commander.Health -= damage;
    }

    public void EndTurn()
    {
        if (isPlayerTurn)
            GameManager.TurnSystem.endTurn = true;
    }
}

