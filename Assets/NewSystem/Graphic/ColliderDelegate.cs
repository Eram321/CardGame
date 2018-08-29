using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDelegate : MonoBehaviour {

    public delegate void PlayerEnter();
    public event PlayerEnter OnPlayerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerOnMapController>();
        if (player)
            OnPlayerEnter();
    }
}
