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
    [SerializeField] float offsetBetwenPlacesY = 1f;

    [SerializeField] GameObject mapHolder;

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
        var size = new Vector2();
        for (int i = 0; i < Lines; i++)
        {
            //Create line 
            var lineHolder = new GameObject(string.Format("Line {0} Holder", i));
            lineHolder.transform.SetParent(mapHolder.transform);
            var line = lineHolder.AddComponent<Line>();
            lines.Add(line);

            for (int j = 0; j < PlacesInLine; j++)
            {
                var placeObject = Instantiate(PlacePrefab, lineHolder.transform);
                size = placeObject.GetComponent<PolygonCollider2D>().bounds.extents * 2f;

                placeObject.transform.localPosition = new Vector3((j + i*0.5f - i/2)*size.x, 0, i);
                var place = placeObject.GetComponent<Place>();
                place.Position = j;

                if (j == 0)
                    PlaceForPlayer(GameManager.TurnSystem.GetPlayer(0), place);
                else if (j==PlacesInLine-1)
                    PlaceForPlayer(GameManager.TurnSystem.GetPlayer(1), place);

                line.AddPlace(place);
            }

            lineHolder.transform.localPosition = new Vector3(0,i+i*0.05f);
        }
    }

    private void PlaceForPlayer(PlayerController player, Place place)
    {
        place.InitLineForPlayer = player;
    }

    public List<Line> GetLines()
    {
        return lines;
    }
}
