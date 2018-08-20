using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    [Header("Map Objects")]
    [SerializeField] GameObject PlacePrefab;

    [Header("Map Parameters")]
    [SerializeField] int Lines;
    [SerializeField] int PlacesInLine;
    [SerializeField] float offsetBetwenPlaces = 1f;

    List<Line> lines = new List<Line>();

    private void Awake()
    {
        if (GameManager.MapController == null)
            GameManager.MapController = this;
        else
            Destroy(this.gameObject);
    }
    void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        //Create map 
        var mapHolder = new GameObject("MapHolder");
        for (int i = 0; i < Lines; i++)
        {
            //Create line 
            var lineHolder = new GameObject(string.Format("Line {0} Holder", i));
            lineHolder.transform.SetParent(mapHolder.transform);
            var line = lineHolder.AddComponent<Line>();
            lines.Add(line);

            var size = new Vector2();
            for (int j = 0; j < PlacesInLine; j++)
            {
                var placeObject = Instantiate(PlacePrefab, lineHolder.transform);
                size = placeObject.GetComponent<BoxCollider2D>().bounds.extents;
                placeObject.transform.position = new Vector3(j * size.x + offsetBetwenPlaces * j * size.x, 0);
                var place = placeObject.GetComponent<Place>();

                if (j == 0)
                    PlaceForPlayer(GameManager.TurnSystem.GetPlayer(0), place);
                else if (j==PlacesInLine-1)
                    PlaceForPlayer(GameManager.TurnSystem.GetPlayer(1), place);

                line.AddPlace(place);
            }

            lineHolder.transform.position = new Vector3(0,i*size.y + offsetBetwenPlaces*i*size.y);
        }

        mapHolder.transform.position = new Vector3((-PlacesInLine+1)/offsetBetwenPlaces, (-Lines + 1) / offsetBetwenPlaces);
    }

    private void PlaceForPlayer(PlayerController player, Place place)
    {
        place.InitLineForPlayer = player;
    }

    public void EnableInit(PlayerController player)
    {
        foreach (var line in lines)
        {
            line.EnableInit(player);
        }
    }
    public void DisableInit(PlayerController player)
    {
        foreach (var line in lines)
        {
            line.DisableInit(player);
        }
    }

    public List<Line> GetLines()
    {
        return lines;
    }
}
