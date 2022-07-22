using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private int lumiPoints;
    [SerializeField] private TMP_Text LumiPointsUI;
    [SerializeField] private GameObject CardSlot;
    [SerializeField] private GameObject DeckPanel;
    [SerializeField] private List<BaseCard> StartingDeck;
    [SerializeField] private List<CardSlot> CurrentDeck;
    [SerializeField] private GameObject LineRenderer;

    public CardSlot Target;
    public enum ActionType {Upgrade, Remove };
    public ActionType actionType;
    public bool Action;
    public int LumiPoints => lumiPoints;


    private void Start()
    {
        LumiUIUpdate();
        SetupCardSlots();
    }

    //SETS UP CARD SLOTS FROM STARTING DECK
    private void SetupCardSlots()
    {
        foreach (BaseCard card in StartingDeck)
        {
            var newCardSlot = Instantiate(CardSlot, DeckPanel.transform);
            var cardSlot = newCardSlot.GetComponent<CardSlot>();
            cardSlot.card = card;
            cardSlot.deckManager = this;
        }
        DeckUpdate();
    }
    //FOLLOWS CURRENT DECK TO EASILY CHECK WHICH CARD SHOULD BE HIGHLIGHTED DURING UPGRADE ACTION
    private void DeckUpdate()
    {
        CurrentDeck = new List<CardSlot>();
        foreach (Transform child in DeckPanel.transform)
            CurrentDeck.Add(child.GetComponent<CardSlot>());
    }
    private void UpgradeCard(CardSlot slot)
    {
        if (slot.CardLevel == 1 && LumiPoints >= slot.card.UpgradeCost)
        {
            lumiPoints -= slot.UpgradeCard();
            LumiUIUpdate();
            DeckUpdate();
        }
        ResetAction();
    }
    
    private void DestroyCard(CardSlot slot)
    {
        lumiPoints += slot.CardLevel == 1 ? slot.card.DestroyPoints : slot.card.DestroyUpgradedPoints;
        LumiUIUpdate();

        Destroy(slot.gameObject);
        ResetAction();
        DeckUpdate();
    }
    private void LumiUIUpdate()
    {
        LumiPointsUI.text = "Lumi: " + LumiPoints.ToString();
    }

    //HIGHLIGHTS CARDS THAT CAN BE UPGRADED
    public void HighlightAvailableCards()
    {
        DeckUpdate();
        foreach (var cardSlot in CurrentDeck)
        {
            if (cardSlot == null)
                return;

            if (cardSlot.CardLevel != 1 || cardSlot.card.UpgradeCost > lumiPoints)
            {
                cardSlot.HideCard();
            }
        }
    }
    public void ResetHighlights()
    {
        foreach (var cardSlot in CurrentDeck)
        {
            if (cardSlot == null)
                return;

            cardSlot.ShowCard();
        }
    }

    //SETS TYPE OF PERFORMED ACTION TO UPGRADE
    public void SetUpgradeAction()
    { 
        Action = true;
        actionType = ActionType.Upgrade;
    }

    //SETS TYPE OF PERFORMED ACTION TO DESTROY
    public void SetRemoveAction()
    {
        Action = true;
        actionType = ActionType.Remove;
    }
    public void ResetAction()
    {
        Action = false;
        ResetHighlights();
        LineRenderer.SetActive(false);
    }

    public void UpgradeTarget()
    {
        if (Target != null)
        {
            UpgradeCard(Target);
            LineRenderer.SetActive(false);
        }
    }
    public void DestroyTarget()
    {
        if (Target != null)
        {
            DestroyCard(Target);
            LineRenderer.SetActive(false);
        }
    }
    public void ActionOnCard()
    {
        switch (actionType)
        {
            case ActionType.Upgrade:
                UpgradeTarget();
                return;
            case ActionType.Remove:
                DestroyTarget();
                return;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ResetAction();
    }
    
}
