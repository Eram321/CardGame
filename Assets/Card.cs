using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Neutral,
    KingdomOfPoland
}
public enum CardRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}


public struct Card {

    public Card(string id, string name, string imageName, string description, int attackPoints, int defensePoints, CardType type, CardRarity rarity)
    {
        ID = id;
        Name = name;
        ImageName = imageName;
        Description = description;
        Type = type;
        Rarity = rarity;
        AttackPoints = attackPoints;
        DefensePoints = defensePoints;
    }

    public string ID;
    public string Name;
    public string ImageName;
    public string Description;

    public int AttackPoints;
    public int DefensePoints;

    public CardType Type;
    public CardRarity Rarity;
}
