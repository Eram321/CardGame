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
    static string animationJsonFilePath = Application.dataPath + "/Resources/unit_animation_info.json";

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
            var name = data[i]["Name"].ToString();
            var imageName = data[i]["ImageName"].ToString();
            var attack = int.Parse(data[i]["Attack"].ToString());
            var defense = int.Parse(data[i]["Defense"].ToString());
            var range = int.Parse(data[i]["Range"].ToString());
            var speed = int.Parse(data[i]["Speed"].ToString());
            var turns = int.Parse(data[i]["Turns"].ToString());
            cards.Add(new Card(id,name, imageName, attack, defense, range, speed, turns));
        }

        return cards;
    }

    public static List<UnitAnimationInfo> ReadUnitAnimationInfo()
    {
        var json = File.ReadAllText(animationJsonFilePath);

        var data = JsonMapper.ToObject(json);
        var info = new List<UnitAnimationInfo>();

        for (int i = 0; i < data.Count; i++)
        {
            var id = int.Parse(data[i]["id"].ToString());

            var size = int.Parse(data[i]["idle"]["size"].ToString());
            var lenght = int.Parse(data[i]["idle"]["lenght"].ToString());
            var start = int.Parse(data[i]["idle"]["start"].ToString());
            var idle = new AnimInfo(size, lenght, start);

            size = int.Parse(data[i]["walk"]["size"].ToString());
            lenght = int.Parse(data[i]["walk"]["lenght"].ToString());
            start = int.Parse(data[i]["walk"]["start"].ToString());
            var walk = new AnimInfo(size, lenght, start);

            size = int.Parse(data[i]["attack"]["size"].ToString());
            lenght = int.Parse(data[i]["attack"]["lenght"].ToString());
            start = int.Parse(data[i]["attack"]["start"].ToString());
            var attack = new AnimInfo(size, lenght, start);

            info.Add(new UnitAnimationInfo(id, idle, walk, attack));
        }

        return info;
    }

    public static List<Deck> ReadAllDecks()
    {
        var json = File.ReadAllText(deckJsonFilePath);
        var data = JsonMapper.ToObject(json);
        var decks = new List<Deck>();

        for (int i = 0; i < data.Count; i++)
        {
            var cardsID = new List<int>();
            var jsonCards = data[i]["CardsID"];
            foreach (var card in jsonCards){
                cardsID.Add(int.Parse(card.ToString()));
            }

            decks.Add(new Deck(cardsID.ToArray()));
        }

        return decks;
    }

    public static void WriteAllDeck(List<Deck> deckList)
    {
        var saveData = JsonMapper.ToJson(deckList);
        File.WriteAllText(deckJsonFilePath, saveData);
    }
}



