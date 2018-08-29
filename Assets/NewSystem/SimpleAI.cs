using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour {

    PlayerController playerController;
    private void OnDestroy(){
        playerController.OnStartTurn -= StartTurn;
    }

    private void OnEnable()
    {
        playerController = GetComponent<PlayerController>();
        playerController.OnStartTurn += StartTurn;
    }

    private void StartTurn()
    {
        playerController.CommanderCards = Data.ReadDeckWithID(Game.Instance.CurrentEnemyDeck);

        StartCoroutine(AI());
    }

    private IEnumerator AI()
    {
        //Wait start turn noise
        Debug.Log("Waiting");
        yield return new WaitForSeconds(1f);

        var count = UnityEngine.Random.Range(1, 4);
        while (count > 0) { 

            //Select unit to place
            Debug.Log("Selecing unit");
            yield return new WaitForSeconds(1f);

            //SIMPLE AI
            //Get random card from deck
            if(playerController.hand.DeckSize > 0) { 
                var rand = UnityEngine.Random.Range(0, playerController.hand.DeckSize);
                var card = playerController.hand.CardAt(rand);
                
                var unit = new GameObject("AI TEMP UNIT");
                var cardObject = unit.AddComponent<CardObject>();
                cardObject.Card = card;

                playerController.selectedUnit = cardObject;

                //Select place
                Debug.Log("Selecing place");
                yield return new WaitForSeconds(1f);

                var ablePlaces = new List<Place>();
                var lines = GameManager.MapController.GetLines();
                foreach (var line in lines)
                {
                    foreach (var placeInLine in line.places)
                    {
                        if (placeInLine.InitLineForPlayer == playerController)
                            ablePlaces.Add(placeInLine);
                    }
                }
                rand = UnityEngine.Random.Range(0, ablePlaces.Count);
                var place = ablePlaces[rand];

                //Call func
                playerController.PlaceAt(place);

                count--;
            }
            else
            {
                //End Turn
                count = 0;
            }

            if(count == 0)
                GameManager.TurnSystem.EndTurn();
        }
    }
}
