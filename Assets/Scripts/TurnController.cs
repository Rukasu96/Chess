using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    private int playerIndex = 0;

    public TeamColor ChangePlayer(TeamSettings[] teamsOrder)
    {
        if(playerIndex >= teamsOrder.Length - 1)
            playerIndex = 0;
        else
            playerIndex++;

        return teamsOrder[playerIndex].teamColor;
    }
}
