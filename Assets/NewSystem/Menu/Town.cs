using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : InteractableObject, IDelegateReciver
{
    public enum TownOptions
    {
        FightEasy,
        FightMedium,
        FightHard,
        Recruit,
        Ruins
    }
    [SerializeField] List<TownOptions> options;

    public void OnRecive()
    {
        Menu.Instance.townWindow.SetWindow(options);
        Menu.Instance.OpenWindow(typeof(TownWindow));
    }
}
