using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Infantry,
    Archers,
    Cavalary
}

public struct Card {

    public Card(int id, string name, string imageName, int attack, int defense, int range, int speed, int turns, int[] upgrades, float expNeeded)
    {
        this.ID = id;
        this.Name = name;
        this.ImageName = imageName;
        this.Attack = attack;
        this.Defense = defense;
        this.Range = range;
        this.Speed = speed;
        this.Turns = turns;
        this.Upgrades = new int[upgrades.Length];
        Array.Copy(upgrades, this.Upgrades, upgrades.Length);
        this.expNeeded = expNeeded;
    }

    public int ID;
    public string ImageName;
    public string Name;
    public int Attack;
    public int Defense;
    public int Range;
    public int Speed;
    public int Turns;
    public int[] Upgrades;
    public float expNeeded;
}
