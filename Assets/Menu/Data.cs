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
            var id = int.Parse(data[i]["ID"].ToString());
            var imageName = data[i]["ImageName"].ToString();
            var typeString = data[i]["Type"].ToString();
            var attackPoints = int.Parse(data[i]["AttackPoints"].ToString());
            var defensePoints = int.Parse(data[i]["DefensePoints"].ToString());
            var vsInf = bool.Parse(data[i]["VsINF"].ToString());
            var vsArch = bool.Parse(data[i]["VsArch"].ToString());
            var vsCav = bool.Parse(data[i]["VsCav"].ToString());

            var type = Enum.Parse(typeof(CardType), typeString);

            cards.Add(new Card(id,imageName,attackPoints,defensePoints,(CardType)type,vsInf, vsArch, vsCav));
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
            var cardsID = new List<int>();
            var jsonCards = data[i]["CardsID"];
            foreach (var card in jsonCards){
                cardsID.Add(int.Parse(card.ToString()));
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



