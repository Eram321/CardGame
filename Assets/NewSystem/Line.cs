using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour {

    public List<Place> places = new List<Place>();

    public List<Place> GetPlaces()
    {
        return places;
    }

    public void AddPlace(Place p){
        places.Add(p);
    }

    public void EnableInit(PlayerController player)
    {
        Debug.Log(player);
        var init = places.Single(p => p.InitLineForPlayer == player);
        init.Enable();
    }
    public void DisableInit(PlayerController player)
    {
        var init = places.Single(p => p.InitLineForPlayer == player);
        init.Disable();
    }
}
