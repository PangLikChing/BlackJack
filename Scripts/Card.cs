using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Card : MonoBehaviour
{
    public int cardSuit;
    public int cardNumber;

    public void setCardSuit(int suit)
    {
        cardSuit = suit;
    }

    public int getCardSuit()
    {
        return cardSuit;
    }

    public void setCardNumber(int number)
    {
        cardNumber = number;
    }

    public int getCardNumber()
    {
        return cardNumber;
    }
}
