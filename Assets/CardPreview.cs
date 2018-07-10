using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPreview : MonoBehaviour {

    [SerializeField] Image cardPreviewImage;
    [SerializeField] Text cardPreviewApText;
    [SerializeField] Text cardPreviewDefText;

    public void ChangeCardPreview(Sprite cardSprite, Card card)
    {
        gameObject.SetActive(true);
        cardPreviewImage.sprite = cardSprite;
        cardPreviewApText.text = card.AttackPoints.ToString();
        cardPreviewDefText.text = card.DefensePoints.ToString();
    }
    public void ToggleCardPreview(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}
