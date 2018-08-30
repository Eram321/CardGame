using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game Instance { get; private set; }

    public PlayerData PlayerData;
    public States States;

    public int CurrentEnemyDeck = 1;

	void Awake () {

        if (Instance == null)
            Instance = this;
        else { 
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        PlayerData = new PlayerData(100, 100, Data.ReadPlayerDeck(), new Vector3(-3.5f,-2.5f,0), 10);
        States = new States();
    }

}

[Serializable]
public struct PlayerData
{
    public int MaxDeckSize;
    public float Experience;
    public float Gold;
    public UI.Deck PlayerDeck;
    public Vector3 PositionOnMap;

    public PlayerData(float exp, float gold, UI.Deck deck, Vector3 positionOnMap, int deckSize)
    {
        MaxDeckSize = deckSize;
        Experience = exp;
        Gold = gold;
        PlayerDeck = deck;
        PositionOnMap = positionOnMap;
    }
}

[Serializable]
public class States
{
    public bool TutorialCompleted;
}
