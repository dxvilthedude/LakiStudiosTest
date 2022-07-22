using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public BaseCard card;
    public DeckManager deckManager;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Image cardIcon;
    [SerializeField] [Range(1, 2)] private int cardLevel;
    private bool isActive = true;
    private Color hidenCard = new Color(1, 1, 1, 0.5f);
    private Color activeCard = new Color(1, 1, 1, 1);
    public int CardLevel => cardLevel;
    public bool IsActive => isActive;
    void Start()
    {
        cardLevel = card.CardLevel;
        SetCardVisuals();
    }

    private void SetCardVisuals()
    {
        cardName.text = cardLevel == 1 ? card.Name : card.UpgradedName;
        cardIcon.sprite = cardLevel == 1 ? card.Icon : card.UpgradedIcon;
        costText.gameObject.SetActive(false);
    }
    public int UpgradeCard()
    {
        cardLevel++;
        SetCardVisuals();

        return card.UpgradeCost;
    }
    public void HideCard()
    {
        cardIcon.color = hidenCard;
        isActive = false;
    }
    public void ShowCard()
    {
        cardIcon.color = activeCard;
        isActive = true;
    }


    // SHOWS COST/LUMI GAIN FROM CHOSEN ACTION (UPGRADE/DESTROY)
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActive && deckManager.Action)
        {
            deckManager.Target = this;
            costText.gameObject.SetActive(true);
            if (deckManager.actionType == DeckManager.ActionType.Upgrade)
            {
                costText.text = "-" + card.UpgradeCost.ToString();
                costText.color = Color.red;
            }
            else
            {
                costText.text = "+" + (cardLevel == 1 ? card.DestroyPoints.ToString() : card.DestroyUpgradedPoints.ToString());
                costText.color = Color.green;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        deckManager.Target = null;
        costText.gameObject.SetActive(false);
    }

    //CALLS ACTION ON CLICK (UPGRADE/DESTROY) IF ANY ACTION IS IN SELECTED
    public void OnPointerClick(PointerEventData eventData)
    {
        if (deckManager.Action)
        {
            deckManager.ActionOnCard();
        }
    }
}
