using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int playerCoins;

    public void setPlayerName(string name)
    {
        playerName = name;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public void setPlayerCoins(int coins)
    {
        playerCoins = coins;
    }

    public int getPlayerCoins()
    {
        return playerCoins;
    }
}
