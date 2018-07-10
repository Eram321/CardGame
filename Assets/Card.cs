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

    public Card(int id, string imageName, int attackPoints, int defensePoints, CardType type, bool vsInf, bool vsArch, bool vsCav)
    {
        ID = id;
        ImageName = imageName;
        Type = type;
        AttackPoints = attackPoints;
        DefensePoints = defensePoints;
        VsINF = vsInf;
        VsCav = vsCav;
        VsArch = vsArch;
    }

    public int ID;
    public string ImageName;

    public int AttackPoints;
    public int DefensePoints;

    public CardType Type;
    public bool VsINF;
    public bool VsArch;
    public bool VsCav;
}
