using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPreviewMenu : MonoBehaviour {

    [SerializeField] Image image;
    [SerializeField] Text name;
    [SerializeField] Text attack;
    [SerializeField] Text defense;
    [SerializeField] Text range;
    [SerializeField] Text speed;

    [SerializeField] UpgradeButton upgrade1;
    [SerializeField] UpgradeButton upgrade2;

    [SerializeField] Text expText;
    [SerializeField] Image expFillImage;

    public Card card;
    public void Set(Card card)
    {
        image.sprite = Resources.Load<Sprite>(string.Format("Cards/{0}/{1}_card", card.ImageName, card.ImageName));
        name.text = card.Name;
        attack.text = card.Attack.ToString();
        defense.text = card.Defense.ToString();
        range.text = card.Range.ToString();
        speed.text = card.Speed.ToString();

        var exp = Game.Instance.PlayerData.PlayerDeck.GetExperience(card.ID, transform.GetSiblingIndex());
        expText.text = string.Format("{0}/{1}", exp, card.expNeeded);
        expFillImage.fillAmount = exp / card.expNeeded;

        if (card.Upgrades.Length == 0)
        {
            upgrade1.SetText("Maximum upgrade reach");
            upgrade1.Toggle(false);
            upgrade2.ToggleObject(false);

            expText.gameObject.SetActive(false);
            expFillImage.fillAmount = 1f;
        }
        else if(card.Upgrades.Length == 1)
        {
            var upg = Data.ReadCardWithID(card.Upgrades[0]);
            upgrade1.SetText("Upgrade to: " + upg.Name);

            upgrade1.ToggleObject(true);
            upgrade2.ToggleObject(false);
        }
        else if(card.Upgrades.Length == 2)
        {
            var upg = Data.ReadCardWithID(card.Upgrades[0]);
            upgrade1.SetText("Upgrade to: " + upg.Name);

            upg = Data.ReadCardWithID(card.Upgrades[1]);
            upgrade2.SetText("Upgrade to: " + upg.Name);

            upgrade1.ToggleObject(true);
            upgrade2.ToggleObject(true);
        }

        
        this.card = card;
    }

    public void FirstUpgradeButton()
    {
        var experience = Game.Instance.PlayerData.PlayerDeck.GetExperience(card.ID, transform.GetSiblingIndex());
        if(experience >= card.expNeeded) { 
            //Upgrade card
            var newCard = Data.UpgradeCard(card, 0, transform.GetSiblingIndex());
            //Update ui
            Menu.Instance.cardsPanel.ReplaceCard(this, newCard);
        }

    }

    public void SecondUpgradeButton()
    {
        var experience = Game.Instance.PlayerData.PlayerDeck.GetExperience(card.ID, transform.GetSiblingIndex());
        if (experience >= card.expNeeded){
            //Upgrade card
            var newCard = Data.UpgradeCard(card, 1, transform.GetSiblingIndex());
            //Update ui
            Menu.Instance.cardsPanel.ReplaceCard(this, newCard);
        }
    }
}
