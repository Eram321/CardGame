using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardObjectPreview : MonoBehaviour {

    [SerializeField] Image image;
    [SerializeField] Text name;
    [SerializeField] Text attack;
    [SerializeField] Text defense;
    [SerializeField] Text range;
    [SerializeField] Text speed;

    public float offsetY = 100f;
    public void Set(CardObject card)
    {
        transform.position = new Vector3(card.transform.position.x, card.transform.position.y + offsetY);
        image.sprite = card.cardImage.sprite;
        name.text = card.Card.Name;
        attack.text = card.Card.Attack.ToString();
        defense.text = card.Card.Defense.ToString();
        range.text = card.Card.Range.ToString();
        speed.text = card.Card.Speed.ToString();
    }
}
