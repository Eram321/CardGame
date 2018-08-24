using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingDamageText : MonoBehaviour {

    [SerializeField] Text UiText;
    public void Init(string text){
        UiText.text = text;
    }

}
