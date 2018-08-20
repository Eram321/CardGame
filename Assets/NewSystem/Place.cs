using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour {

    [SerializeField] GameObject unitPrefab;

    public PlayerController InitLineForPlayer;
    public Unit unit;

    public Unit DropCard(CardObject card, PlayerController player)
    {
        if (unit != null || InitLineForPlayer == null || player != InitLineForPlayer) return null;

        //na podstawie karty stwórz obiekt 
        var unitObject = Instantiate(unitPrefab, transform);
        unitObject.transform.position = transform.position;
        unit = unitObject.GetComponent<Unit>();
        unit.Card = card.Card;
        Disable();

        return unit;
    }

    public void Enable()
    {
        if(unit == null)
            GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    public void Disable()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void MoveUnit(Unit unit)
    {
        this.unit = unit;
        unit.transform.SetParent(transform);
        unit.transform.position = transform.position;
    }
}
