using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchPanel : MonoBehaviour {

    [Header("SearchPanel")]
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform contentParent;
    [SerializeField] InputField searchInputField;

    [Header("PreviewPanel")]
    [SerializeField] MenuCardUC cardPreview;
    [SerializeField] Text cardDescription;

    List<MenuCardUC> menuCardUCs = new List<MenuCardUC>();

    void Start()
    {
        //searching input field listener
        searchInputField.onValueChanged.AddListener(delegate { SearchCards(); });

        MenuCardUC.onMouseEnter += MousePointerEnterCard;
        MenuDeckCardUC.onMouseEnter += MousePointerEnterCard;
        Data.onCardsDataChange += CardsDataChange;

        //Create cards object in main card view
        LoadCardGridView();
    }

    private void LoadCardGridView()
    {
        ClearCardGridView();

        var cards = Data.ReadAllCards();
        foreach (Card card in cards)
        {
            var obj = Instantiate(cardPrefab, contentParent);
            var c = obj.GetComponent<MenuCardUC>();
            c.ItemCard = card;
            menuCardUCs.Add(c);
        }
    }

    private void ClearCardGridView()
    {
        foreach (var item in menuCardUCs){
            Destroy(item.gameObject);
        }
        menuCardUCs.Clear();
    }

    private void SearchCards()
    {
        var quote = searchInputField.text;

        foreach (MenuCardUC item in menuCardUCs)
        {
            if (!item.ItemCard.Name.StartsWith(quote))
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                item.gameObject.SetActive(true);
            }
        }
    }

    private void MousePointerEnterCard(Card card, Image cardImage)
    {
        cardPreview.SetCardPreview(card, cardImage.sprite);
        cardDescription.text = "''" + card.Description + "''";
    }

    private void CardsDataChange(){
        LoadCardGridView();
    }

    void OnDestroy(){
        MenuCardUC.onMouseEnter -= MousePointerEnterCard;
        MenuDeckCardUC.onMouseEnter -= MousePointerEnterCard;
        Data.onCardsDataChange -= CardsDataChange;
    }
}
