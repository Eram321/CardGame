using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using System.Text;
using UI;

public class Data {

    static string cardJsonFilePath = Application.dataPath + "/Resources/cards.json";
    static string deckJsonFilePath = Application.dataPath + "/Resources/decks.json";

    public delegate void CardsDataChange();
    public static event CardsDataChange onCardsDataChange;

    public static void AddNewCard(Card card)
    {
        var data = ReadAllCards();
        data.Add(card);

        var saveData = JsonMapper.ToJson(data);
        File.WriteAllText(cardJsonFilePath, saveData);

        onCardsDataChange();
    }
    public static List<Card> ReadAllCards()
    {
        var json = File.ReadAllText(cardJsonFilePath);

        var data = JsonMapper.ToObject(json);
        var cards = new List<Card>();

        for (int i = 0; i < data.Count; i++)
        {
            var id = data[i]["ID"].ToString();
            var name = data[i]["Name"].ToString();
            var imageName = data[i]["ImageName"].ToString();
            var description = data[i]["Description"].ToString();
            var typeString = data[i]["Type"].ToString();
            var rarityString = data[i]["Rarity"].ToString();
            var attackPoints = int.Parse(data[i]["AttackPoints"].ToString());
            var defensePoints = int.Parse(data[i]["DefensePoints"].ToString());

            var type = Enum.Parse(typeof(CardType), typeString);
            var rarity = Enum.Parse(typeof(CardRarity), rarityString);

            cards.Add(new Card(id,name,imageName,description,attackPoints,defensePoints,(CardType)type,(CardRarity)rarity));
        }

        return cards;
    }

    public static List<Deck> ReadAllDecks()
    {
        var json = File.ReadAllText(deckJsonFilePath);
        var data = JsonMapper.ToObject(json);
        var decks = new List<Deck>();

        for (int i = 0; i < data.Count; i++)
        {
            var name = data[i]["Name"].ToString();
            var typeString = data[i]["Type"].ToString();

            var cardsID = new List<string>();
            var jsonCards = data[i]["CardsID"];
            foreach (var card in jsonCards){
                cardsID.Add(card.ToString());
            }

            var type = Enum.Parse(typeof(CardType), typeString);

            decks.Add(new Deck(name, (CardType)type, cardsID.ToArray()));
        }

        return decks;
    }

    public static void WriteAllDeck(List<Deck> deckList)
    {
        var saveData = JsonMapper.ToJson(deckList);
        File.WriteAllText(deckJsonFilePath, saveData);
    }
}



