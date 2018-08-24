using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CardType
//{
//    Neutral,
//    KingdomOfPoland
//}
//public enum CardRarity
//{
//    Common,
//    Rare,
//    Epic,
//    Legendary
//}
public enum CardType
{
    Infantry,
    Archers,
    Cavalary
}


public struct Card {

    //public Card(string id, string name, string imageName, string description, int attackPoints, int defensePoints, CardType type, CardRarity rarity)
    //{
    //    ID = id;
    //    Name = name;
    //    ImageName = imageName;
    //    Description = description;
    //    Type = type;
    //    Rarity = rarity;
    //    AttackPoints = attackPoints;
    //    DefensePoints = defensePoints;
    //}

    public Card(int id, string name, string imageName, int attack, int defense, int range, int speed, int turns)
    {
        this.ID = id;
        this.Name = name;
        this.ImageName = imageName;
        this.Attack = attack;
        this.Defense = defense;
        this.Range = range;
        this.Speed = speed;
        this.Turns = turns;
    }

    public int ID;
    public string ImageName;
    public string Name;
    public int Attack;
    public int Defense;
    public int Range;
    public int Speed;
    public int Turns;
}
