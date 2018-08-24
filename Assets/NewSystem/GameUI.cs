using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    [SerializeField] CardObjectPreview cardPreviewPanel;
    [SerializeField] Text turnTimeText;

    void Awake()
    {
        if (GameManager.GameUI == null)
            GameManager.GameUI = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        cardPreviewPanel.gameObject.SetActive(false);
    }

    public void SetCardPreview(CardObject card)
    {
        cardPreviewPanel.gameObject.SetActive(true);

        cardPreviewPanel.Set(card);
    }

    public void DisableCardPreview()
    {
        cardPreviewPanel.gameObject.SetActive(false);
    }

    public void ResetUI()
    {
        DisableCardPreview();
    }

    public void SetTurnTime(float value)
    {
        turnTimeText.text = value.ToString();
    }
}
