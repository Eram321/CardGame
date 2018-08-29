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
}
