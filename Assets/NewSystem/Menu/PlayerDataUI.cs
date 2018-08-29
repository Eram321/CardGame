using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataUI : MonoBehaviour {

    [SerializeField] Text goldText;

    private void Start()
    {
        UpdateGoldText();
    }

    public void UpdateGoldText()
    {
        goldText.text = Game.Instance.PlayerData.Gold.ToString();
    }

}
