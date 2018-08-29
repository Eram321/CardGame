using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitsController : MonoBehaviour
{
    public void DisablePlaces(PlayerController player)
    {
        var lines = GameManager.MapController.GetLines();
        foreach (var line in lines)
        {
            var places = line.GetPlaces();
            var place = places.Single(p => p.InitLineForPlayer == player);
            place.Disable();
        }
    }

    public void EnablePlaces(PlayerController player)
    {
        var lines = GameManager.MapController.GetLines();
        foreach (var line in lines)
        {
            var places = line.GetPlaces();
            var place = places.Single(p => p.InitLineForPlayer == player);
            if (place.unit == null)
                place.Enable();
        }
    }

    public bool PlaceCard(Place place, CardObject selectedUnit, PlayerController player, List<Unit> units)
    {
        var dropped = place.DropCard(selectedUnit, player);
        if (dropped)
        {
            //Drop unit
            units.Add(dropped);
            DisablePlaces(player);
            return true;
        }

        return false;
    }
}
