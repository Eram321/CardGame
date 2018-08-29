using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using System.Text;
using UI;
using System.Linq;

public class Data {

    static string cardJsonFilePath = Application.dataPath + "/Resources/cards.json";
    static string deckJsonFilePath = Application.dataPath + "/Resources/decks.json";
    static string animationJsonFilePath = Application.dataPath + "/Resources/unit_animation_info.json";

   

    public delegate void CardsDataChange();
    public static event CardsDataChange onCardsDataChange;

    internal static Deck ReadDeckWithID(int currentEnemyDeck)
    {
        return ReadAllDecks()[currentEnemyDeck];
    }

    internal static Deck ReadPlayerDeck()
    {
        return ReadAllDecks()[0];
    }

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

            var list = new List<int>();
            var upgrades = data[i]["Upgrades"];
            foreach (var upg in upgrades){
                list.Add(int.Parse(upg.ToString()));
            }

            var exp = int.Parse(data[i]["ExpNeed"].ToString());

            cards.Add(new Card(id,name, imageName, attack, defense, range, speed, turns, list.ToArray(), exp));
        }

        return cards;
    }

    internal static void AddNewCard()
    {
        Game.Instance.PlayerData.PlayerDeck.AddNewCard();

        SavePlayerDeck();
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

    internal static Card UpgradeCard(Card card, int upgradeNumber, int index)
    {
        var newCard = ReadCardWithID(card.Upgrades[upgradeNumber]);
        Game.Instance.PlayerData.PlayerDeck.UpgradeCard(card, newCard, index);

        SavePlayerDeck();

        return newCard;
    }

    public static Card ReadCardWithID(int id)
    {
        var cards = ReadAllCards();
        return cards.SingleOrDefault(c => c.ID == id);
    }

    public static List<Deck> ReadAllDecks()
    {
        var json = File.ReadAllText(deckJsonFilePath);
        var data = JsonMapper.ToObject(json);
        var decks = new List<Deck>();

        for (int i = 0; i < data.Count; i++)
        {
            var list = new List<DeckCard>();
            for (int j = 0; j < data[i]["Cards"].Count; j++)
            {
                var id = int.Parse(data[i]["Cards"][j]["ID"].ToString());
                var exp = int.Parse(data[i]["Cards"][j]["Experience"].ToString());
                list.Add(new DeckCard(id, exp));
            }

            decks.Add(new Deck(list));
        }

        return decks;
    }

    public static void SavePlayerDeck()
    {
        var decks = ReadAllDecks();
        decks[0] = Game.Instance.PlayerData.PlayerDeck;
        var saveData = JsonMapper.ToJson(decks);
        File.WriteAllText(deckJsonFilePath, saveData);
    }
}



