using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : InteractableObject
{
    [SerializeField] ColliderDelegate deleage;

    private void OnEnable()
    {
        deleage.OnPlayerEnter += InteractWithPlayer;
    }
    private void OnDestroy()
    {
        deleage.OnPlayerEnter -= InteractWithPlayer;
    }

    private void InteractWithPlayer()
    {
        Menu.Instance.OpenWindow(typeof(TownWindow));
    }
}
