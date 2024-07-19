using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField] private TeamSettings[] teamsOrder;
    private TeamColor activePlayer;
    private int playerIndex;

    private void Start()
    {
        playerIndex = 0;
        activePlayer = teamsOrder[playerIndex].teamColor;
    }
    private void ChangePlayer()
    {
        if(playerIndex >= teamsOrder.Length)
            playerIndex = 0;
        else
            playerIndex++;

        activePlayer = teamsOrder[playerIndex].teamColor;
    }
}
