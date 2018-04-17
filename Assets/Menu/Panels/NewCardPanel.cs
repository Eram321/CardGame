using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCardPanel : MonoBehaviour {

    //Panel
    [SerializeField] GameObject newCardPanel;

    //Buttons
    [SerializeField] Button addCardButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Button submitButton;

    //Input fields
    [SerializeField] InputField idInputField;
    [SerializeField] InputField nameInputField;
    [SerializeField] InputField imageNameInputField;
    [SerializeField] InputField apInputField;
    [SerializeField] InputField dfInputField;
    [SerializeField] InputField descriptionInputField;
    [SerializeField] Dropdown typeDropdownList;
    [SerializeField] Dropdown rarityDropdownList;

    void Start () {

        //Connect buttons listeners
        addCardButton.onClick.AddListener(AddCardButtonClick);
        cancelButton.onClick.AddListener(CancelButtonClick);
        submitButton.onClick.AddListener(SubmitButtonClick);
    }

    public void AddCardButtonClick()
    {
        newCardPanel.SetActive(true);
        newCardPanel.transform.SetAsLastSibling();
    }
    public void CancelButtonClick(){
        newCardPanel.SetActive(false);
    }
    public void SubmitButtonClick()
    {
        var id = idInputField.text;
        var name = nameInputField.text;
        var iName = imageNameInputField.text;
        var desc = descriptionInputField.text;

        var ap = int.Parse(apInputField.text);
        var df = int.Parse(dfInputField.text);

        var type = (CardType)typeDropdownList.value;
        var rarity = (CardRarity)rarityDropdownList.value;

        Data.AddNewCard(new Card(id, name, iName, desc, ap, df, type, rarity));

        CancelButtonClick();
    }
}
