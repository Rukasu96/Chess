using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromotionManager : MonoBehaviour
{
    [SerializeField] private PromotionController controller;
    [SerializeField] private TurnManager turnManager;

    public void CheckChessmanOnPromotionTile(Chessman chessman, Tile currentTile)
    {
        if (turnManager.ActivePlayer == TeamColor.white && currentTile.PositionOnGrid.posX == 8)
        {
            controller.StartPromotion(chessman, turnManager.ActivePlayer);
        } else if (currentTile.PositionOnGrid.posX == 0)
        {
            controller.StartPromotion(chessman, turnManager.ActivePlayer);
        }
    }
}
