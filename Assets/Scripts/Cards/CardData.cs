using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card data", menuName = "Create new card data")]
public class CardData : ScriptableObject
{
    public CardType CurrentCardType;
    public string CardName;
    public string CardDescription;
    public int CardCost;
    public Sprite CardLogo;
}

public enum CardType { Battle, Exploration}
