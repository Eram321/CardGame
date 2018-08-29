using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

    [SerializeField] Button button;
    [SerializeField] Text text;

    public void SetText(string txt)
    {
        text.text = txt;
    }

    public void Toggle(bool v)
    {
        gameObject.SetActive(true);
        button.interactable = v;
    }

    public void ToggleObject(bool v)
    {
        gameObject.SetActive(v);
    }
}
