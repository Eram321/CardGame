using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour {

    [SerializeField] Button CloseButton;

    public void Start(){
        CloseButton.onClick.AddListener(ButtonClick);
    }
    public void OnDestroy(){
        CloseButton.onClick.RemoveListener(ButtonClick);
    }

    public virtual void ButtonClick()
    {
        Menu.Instance.CloseWindow();
    }
}
