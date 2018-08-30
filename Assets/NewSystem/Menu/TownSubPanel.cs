using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownSubPanel : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] Button accept;
    [SerializeField] Button cancel;

    public void OpenPanel()
    {
        gameObject.SetActive(true);

        if(Game.Instance.PlayerData.Gold >= 40)
        {

            if (Game.Instance.PlayerData.PlayerDeck.Size < Game.Instance.PlayerData.MaxDeckSize)
            {
                text.text = "Rekrutuj chłopa?(40)";
                accept.gameObject.SetActive(true);
                cancel.gameObject.SetActive(true);
            }
            else
            {
                text.text = "Maksymalny rozmiar odziału";
                accept.gameObject.SetActive(false);
                cancel.gameObject.SetActive(true);
            }
        }
        else
        {
            text.text = "Niewystarczająca ilość złota";
            accept.gameObject.SetActive(false);
            cancel.gameObject.SetActive(true);
        }
    }

    public void AcceptRecruit()
    {
        Game.Instance.PlayerData.Gold -= 40;
        Menu.Instance.topPanel.UpdateGoldText();
        Data.AddNewCard();
        gameObject.SetActive(false);
    }
}
