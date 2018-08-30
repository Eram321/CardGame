using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour {

    [SerializeField] Text goldText;

    public void OpenTroopsWindow()
    {
        Menu.Instance.OpenWindow(typeof(CardsPanel));
    }

    private void Start()
    {
        UpdateGoldText();
    }

    public void UpdateGoldText()
    {
        goldText.text = Game.Instance.PlayerData.Gold.ToString();
    }

}
