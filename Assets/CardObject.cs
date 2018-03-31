using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public int HERO_CARD_ID;

    void Start()
    {
        Hero.onHeroEndTurn += TurnEnd;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(HERO_CARD_ID == Game.Core.TurnSystem.Instance.CURRENT_HERO_ID) { 
            transform.localScale = new Vector2(2, 2);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (HERO_CARD_ID == Game.Core.TurnSystem.Instance.CURRENT_HERO_ID){
            transform.localScale = new Vector2(1, 1);
        }
    }

    void TurnEnd()
    {
        transform.localScale = new Vector2(1, 1);
    }

}
