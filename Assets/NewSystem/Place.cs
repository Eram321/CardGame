using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour {

    [SerializeField] GameObject unitPrefab;

    public PlayerController InitLineForPlayer;
    public Unit unit;

    public bool Enabled;
    public int Position;

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
        GetComponent<SpriteRenderer>().color = Color.yellow;
        Enabled = true;
    }
    public void Disable()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Enabled = false;
    }

    public IEnumerator MoveUnit(Unit unit, bool flip)
    {
        var speed = 2;
        unit.transform.SetParent(null);
        var dist = Vector3.Distance(unit.transform.position, transform.position);
        while(dist > 0.1f)
        {
            unit.ToggleWalkAnimation(true, flip);
            dist = Vector3.Distance(unit.transform.position, transform.position);
            unit.transform.position = Vector3.MoveTowards(unit.transform.position, transform.position, Time.deltaTime*speed);
            yield return new WaitForEndOfFrame();
        }

        unit.ToggleWalkAnimation(false, flip);
        this.unit = unit;
        unit.transform.SetParent(transform);
        unit.transform.position = transform.position;
    }
}
