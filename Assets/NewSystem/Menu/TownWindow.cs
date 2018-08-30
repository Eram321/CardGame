using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TownWindow : Window {

    [SerializeField] Button RecruitButton;
    [SerializeField] Button FightOnArena;
    [SerializeField] Button ExploreRuins;

    [SerializeField] TownSubPanel townSubPanel;

    public void Recruit()
    {
        townSubPanel.OpenPanel();
        RecruitButton.transform.parent.gameObject.SetActive(false);
    }

    public void SetWindow(List<Town.TownOptions> options)
    {
        FightOnArena.gameObject.SetActive(false);
        RecruitButton.gameObject.SetActive(false);
        ExploreRuins.gameObject.SetActive(false);
        townSubPanel.gameObject.SetActive(false);

        foreach (var option in options)
        {
            switch (option)
            {
                case Town.TownOptions.FightEasy:
                    FightOnArena.gameObject.SetActive(true);
                    var txt = FightOnArena.GetComponentInChildren<Text>();
                    txt.text = "Walcz na arenie(Łatwy)";
                    break;
                case Town.TownOptions.FightMedium:
                    FightOnArena.gameObject.SetActive(true);
                    txt = FightOnArena.GetComponentInChildren<Text>();
                    txt.text = "Walcz na arenie(Średni)";
                    break;
                case Town.TownOptions.FightHard:
                    FightOnArena.gameObject.SetActive(true);
                    txt = FightOnArena.GetComponentInChildren<Text>();
                    txt.text = "Walcz na arenie(Trudny)";
                    break;
                case Town.TownOptions.Recruit:
                    RecruitButton.gameObject.SetActive(true);
                    break;
                case Town.TownOptions.Ruins:
                    ExploreRuins.gameObject.SetActive(true);
                    break;
            }
        }
    }

    public void Fight()
    {
        var txt = FightOnArena.GetComponentInChildren<Text>();
        switch (txt.text)
        {
            case "Walcz na arenie(Łatwy)":
                Game.Instance.CurrentEnemyDeck = 1;
                break;

            case "Walcz na arenie(Średni)":
                Game.Instance.CurrentEnemyDeck = 2;
                break;

            case "Walcz na arenie(Trudny)":
                Game.Instance.CurrentEnemyDeck = 3;
                break;
        }

        SceneManager.LoadScene("BattleMap");
    }

    public void Explore()
    {
        Game.Instance.CurrentEnemyDeck = 4;
        SceneManager.LoadScene("BattleMap");
    }

}
