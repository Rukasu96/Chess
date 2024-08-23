using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman, IBonusMoveable
{
    private PositionOnGrid startingPosition;

    private void Start()
    {
        boardManager = FindAnyObjectByType<BoardManager>();
        startingPosition = position;
    }

    public Tile AddBonusTileToMove()
    {
        var movePatterns = chessmanSO.Name.movePatterns;
        if(teamColor == TeamColor.white)
        {
            return boardManager.ReturnTile(position.posX + movePatterns[0].posX + 1, position.posZ);
        }
        return boardManager.ReturnTile(position.posX - movePatterns[0].posX - 1, position.posZ);
    }
    public bool IsOnStartingPosition()
    {
        if(startingPosition.posX == position.posX && startingPosition.posZ == position.posZ)
        {
            return true;
        }

        return false;
    }

    public override List<Tile> GetAvailableTilesToMove()
    {
        List<Tile> availableTiles = new List<Tile>();

        if (IsOnStartingPosition())
        {
            availableTiles = base.GetAvailableTilesToMove();
            availableTiles.Add(AddBonusTileToMove());
            return availableTiles;
        }

        return base.GetAvailableTilesToMove();
    }
}
