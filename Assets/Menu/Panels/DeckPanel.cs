using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckPanel : MonoBehaviour {

    //Panel
    [SerializeField] GameObject deckPanel;
    [SerializeField] GameObject newDeckPanel;

    //Buttons
    [SerializeField] Button cancelButton;
    [SerializeField] Button submitButton;

    //Input Fields
    [SerializeField] InputField nameInputField;

    //Prefabs
    [SerializeField] GameObject deckPrefab;
    [SerializeField] GameObject deckCardsPrefab;

    //Contents
    [SerializeField] Transform editDeckContentParent;
    [SerializeField] Transform deckContentParent;

    //Text Fields
    [SerializeField] Text cardsInDeckTextField;

    List<MenuDeckUC> menuDeckUCs = new List<MenuDeckUC>();

    UI.Deck currentDeck;

    int deckMaxSize = 30;
    bool newDeck = false;

    // Use this for initialization
    void Start ()
    {
        //Connect buttons listeners
        cancelButton.onClick.AddListener(CancelButtonClick);
        submitButton.onClick.AddListener(SubmitButtonClick);

        MenuDeckUC.onMouseClick += DeckMouseClick;
        MenuCardUC.onMouseClick += CardMouseClick;
        MenuDeckCardUC.onMouseClick += SetCardInDeckTextWithDelay;

        LoadDeckPanelView();
    }


    private void LoadDeckPanelView()
    {
        var allDecks = Data.ReadAllDecks();
        foreach (UI.Deck deck in allDecks)
        {
            var obj = Instantiate(deckPrefab, deckContentParent);
            var c = obj.GetComponent<MenuDeckUC>();
            c.ItemDeck = deck;
            c.transform.SetAsFirstSibling();
            menuDeckUCs.Add(c);
        }

        CancelButtonClick();
    }

    private void SubmitButtonClick()
    {
        var list = new List<string>();
        foreach (Transform child in editDeckContentParent)
        {
            var deckCard = child.GetComponent<MenuDeckCardUC>();
            list.Add(deckCard.card.ID);
        }

        currentDeck.Name = nameInputField.text;
        currentDeck.CardsID = list.ToArray();

        if (newDeck)
        {
            var obj = Instantiate(deckPrefab, deckContentParent);
            var c = obj.GetComponent<MenuDeckUC>();
            c.ItemDeck = currentDeck;
            c.transform.SetAsFirstSibling();
            menuDeckUCs.Add(c);
        }

        var deckList = new List<UI.Deck>();
        foreach (var deckUC in menuDeckUCs) {
            deckUC.UpdateDeckName();
            deckList.Add(deckUC.ItemDeck);
        }

         Data.WriteAllDeck(deckList);

        CancelButtonClick();
    }

    private void CancelButtonClick()
    {
        newDeckPanel.SetActive(false);
        deckPanel.SetActive(true);

        foreach(Transform child in editDeckContentParent){
            Destroy(child.gameObject);
        }
    }

    private void CardMouseClick(string cardID)
    {
        if (!newDeckPanel.activeSelf) return;
        if (editDeckContentParent.childCount >= deckMaxSize) return;

        var obj = Instantiate(deckCardsPrefab, editDeckContentParent);
        var c = obj.GetComponent<MenuDeckCardUC>();
        c.SetCardInfo(cardID);

        SetCardInDeckText();
    }

    private void DeckMouseClick(UI.Deck deck)
    {
        newDeckPanel.SetActive(true);
        deckPanel.SetActive(false);

        if (deck == null) {
            deck = new UI.Deck("Nowa Talia", CardType.KingdomOfPoland, new string[] { });
            newDeck = true;
        } else
            newDeck = false;

        currentDeck = deck;

        nameInputField.text = deck.Name;

        foreach (string cardID in deck.CardsID)
        {
            var obj = Instantiate(deckCardsPrefab, editDeckContentParent);
            var c = obj.GetComponent<MenuDeckCardUC>();
            c.SetCardInfo(cardID);
        }

        SetCardInDeckText();
    }
    private void SetCardInDeckText()
    {
        cardsInDeckTextField.text = editDeckContentParent.childCount + "/30";
    }
    private void SetCardInDeckTextWithDelay()
    {
        cardsInDeckTextField.text = editDeckContentParent.childCount-1 + "/30";
    }

    void OnDestroy()
    {
        MenuDeckUC.onMouseClick -= DeckMouseClick;
        MenuCardUC.onMouseClick -= CardMouseClick;
        MenuDeckCardUC.onMouseClick -= SetCardInDeckTextWithDelay;
    }

}
