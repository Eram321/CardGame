using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    [SerializeField] Text attackText;
    [SerializeField] Text defenseText;

    [SerializeField] GameObject hitParticle;

    SpriteRenderer spriteRenderer;
    UnitAnimation unitAnimation;

    private Card card;
    public Card Card
    {
        get { return card; }
        set {
            card = value;

            unitAnimation.Init(card);
            attackText.text = card.Attack.ToString();
            defenseText.text = card.Defense.ToString();
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        unitAnimation = GetComponent<UnitAnimation>();
    }

    public bool CalculateDamage(Unit unit)
    {
        CreateHitEffect(unit.card.Attack);

        card.Defense -= unit.Card.Attack;
        defenseText.text = card.Defense.ToString();
        if (card.Defense <= 0) return true;

        return false;
    }

    private void CreateHitEffect(float damage)
    {
        // Instantiate and destroy hit particle
        var hiteffect = Instantiate(hitParticle, transform.position, Quaternion.identity);
        var floatingText = hiteffect.GetComponentInChildren<FloatingDamageText>();
        floatingText.Init(damage.ToString());

        Destroy(hiteffect, 1f);
    }

    bool previewEnable = false;
    public void ToggleWalkAnimation(bool enable, bool flip)
    {
        if(previewEnable != enable) { 
            previewEnable = enable;
            spriteRenderer.flipX = flip;
            if (enable) unitAnimation.PlayWalkAnimation();
            else unitAnimation.PlayIdleAnimation();
        }
    }

    public IEnumerator PlayAttackAnimation(bool flip)
    {
        spriteRenderer.flipX = flip;
        yield return StartCoroutine(unitAnimation.PlayAttackAnimation());
    }
}
