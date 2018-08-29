using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AbilityController : MonoBehaviour {

    public void DisablePlaces()
    {
        var lines = GameManager.MapController.GetLines();
        foreach (var line in lines)
        {
            var places = line.GetPlaces();
            foreach (var place in places){
                place.Disable();
            }
        }
    }

    public void EnablePlaces()
    {
        var lines = GameManager.MapController.GetLines();
        foreach (var line in lines)
        {
            var places = line.GetPlaces();
            foreach (var place in places){
                if (place.unit)
                    place.Enable();
            }
        }
    }

    public void UseAbility(Place place)
    {
        if (place.Enabled)
        {
            DisablePlaces();
            //switch card type
            MoveUnit(place);
        }
    }

    private void MoveUnit(Place place)
    {
        var lines = GameManager.MapController.GetLines();
        foreach (var line in lines)
        {
        }
    }
}
