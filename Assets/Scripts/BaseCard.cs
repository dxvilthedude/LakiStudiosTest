using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/New Card")]
public class BaseCard : ScriptableObject
{
    // BASIC CARD INFO + UPGRADED CARD INFO

    [Header("Basic Info")]
    [SerializeField] private new string name = "Card name";
    [SerializeField] private string upgradedName = "Upgraded card name";
    [SerializeField] private Sprite icon = null;
    [SerializeField] private Sprite upgradedIcon = null;
    [SerializeField] private int upgradeCost;
    [SerializeField] private int destroyPoints;
    [SerializeField] private int destroyUpgradedPoints;
    [SerializeField][Range(1,2)] private int cardLevel;

      
    public string Name => name;
    public string UpgradedName => upgradedName;
    public Sprite Icon => icon;
    public Sprite UpgradedIcon => upgradedIcon;
    public int UpgradeCost => upgradeCost;
    public int DestroyPoints => destroyPoints;
    public int DestroyUpgradedPoints => destroyUpgradedPoints;
    public int CardLevel => cardLevel;

}
