using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

    public static Menu Instance { get; private set; }

    public TopPanel topPanel;
    public CardsPanel cardsPanel;
    public TutorialWindow tutorialWindow;
    public TownWindow townWindow;

    public bool IsOpen;
    Window currentWindow;

    public void ResetSave()
    {
        var list = new List<DeckCard>();
        var card = new DeckCard(0, 0f);
        list.Add(card);
        card = new DeckCard(0, 0f);
        list.Add(card);
        card = new DeckCard(0, 0f);
        list.Add(card);
        card = new DeckCard(1, 0f);
        list.Add(card);
        card = new DeckCard(1, 0f);
        list.Add(card);
        card = new DeckCard(1, 0f);
        list.Add(card);
        card = new DeckCard(6, 0f);
        list.Add(card);
        card = new DeckCard(6, 0f);
        list.Add(card);

        var deck = new UI.Deck(list);
        Game.Instance.PlayerData.PlayerDeck = deck;
        Data.SavePlayerDeck();
    }
    void OnGUI()
    {
    #if UNITY_EDITOR
        if (GUI.Button(new Rect(100, 100, 100, 50), "Reset Save"))
            ResetSave();
    #endif
    }

    private void Start()
    {
        //if (!Game.Instance.States.TutorialCompleted) 
        //    OpenWindow(typeof(TutorialWindow));
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void OpenWindow(Type window)
    {
        if (currentWindow != null) return;

        currentWindow = GetWindow(window);
        currentWindow.gameObject.SetActive(true);
        IsOpen = true;
    }

    private Window GetWindow(Type window)
    {
        if(window == typeof(TutorialWindow))
            return tutorialWindow;
        else if(window == typeof(CardsPanel))
            return cardsPanel;
        else if (window == typeof(TownWindow))
            return townWindow;

        return null;
    }

    public void CloseWindow()
    {
        currentWindow.gameObject.SetActive(false);
        currentWindow = null;
        IsOpen = false;
    }
}
