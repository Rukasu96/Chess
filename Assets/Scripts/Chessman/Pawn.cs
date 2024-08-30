using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman, IBonusMoveable
{
    private PositionOnGrid startingPosition;
    private bool usedBonusMove;

    private void Start()
    {
        boardManager = FindAnyObjectByType<BoardManager>();
        startingPosition = position;
    }

    private Tile[] ReturnBeatEnPassantTiles()
    {
        //Dzia³a ale chujowo napisany kod!!!!! WeŸ to póŸniej zmieñ jak bêdzie ju¿ gameManager. Ta opcja ma byæ tylko dostêpna instant jak pionek wykona ruch o 2 pola 
        Tile[] beatEnPassantTiles = new Tile[2];
        Chessman[] neighbourChessmans = new Chessman[2];
        Tile beatEnPassantTile;
        int id = 0;

        for (int i = 0; i < neighbourChessmans.Length; i++)
        {
            Chessman neighbourChessman;

            if(i == 0)
            {
                neighbourChessman = boardManager.ReturnTile(position.posX, position.posZ + 1).Chessman;
            }
            else
            {
                neighbourChessman = boardManager.ReturnTile(position.posX, position.posZ - 1).Chessman;
            }

            neighbourChessmans[i] = neighbourChessman;
        }

        foreach (Chessman neighbourChessman in neighbourChessmans)
        {
            if(neighbourChessman == null)
            {
                break;
            }

            IBonusMoveable neighbourChessmanWithBonusMove = neighbourChessman.GetComponent<IBonusMoveable>();

            if (neighbourChessmanWithBonusMove == null || !neighbourChessmanWithBonusMove.UsedBonusMove())
            {
                break;
            }

            if(neighbourChessman.GetTeamColor() == TeamColor.white)
            {
                beatEnPassantTile = boardManager.ReturnTile(neighbourChessman.GetPosition().posX - 1, neighbourChessman.GetPosition().posZ);
            }
            else
            {
                beatEnPassantTile = boardManager.ReturnTile(neighbourChessman.GetPosition().posX + 1, neighbourChessman.GetPosition().posZ);
            }

            beatEnPassantTiles[id] = beatEnPassantTile;
            id++;
        }
        
        return beatEnPassantTiles;
    }
    private Tile[] ReturnCombatTiles()
    {
        Tile[] combatTiles = new Tile[2];

        if (teamColor == TeamColor.white)
        {
            combatTiles[0] = boardManager.ReturnTile(position.posX + 1, position.posZ - 1);
            combatTiles[1] = boardManager.ReturnTile(position.posX + 1, position.posZ + 1);
        }
        else
        {
            combatTiles[0] = boardManager.ReturnTile(position.posX - 1, position.posZ - 1);
            combatTiles[1] = boardManager.ReturnTile(position.posX - 1, position.posZ + 1);
        }

        return combatTiles;
    }

    public Tile ReturnBonusTile()
    {
        var movePatterns = chessmanSO.Name.movePatterns;
        Tile bonusTile;
        if(teamColor == TeamColor.white)
        {
            bonusTile = boardManager.ReturnTile(position.posX + movePatterns[0].posX + 1, position.posZ);
        }
        else
        {
            bonusTile = boardManager.ReturnTile(position.posX - movePatterns[0].posX - 1, position.posZ);
        }

        return bonusTile;
    }
    public bool IsOnStartingPosition()
    {
        if(startingPosition.posX == position.posX && startingPosition.posZ == position.posZ)
        {
            return true;
        }

        return false;
    }
    public bool UsedBonusMove()
    {
        return usedBonusMove;
    }

    public override List<Tile> GetAvailableTilesToMove()
    {
        List<Tile> availableTiles = new List<Tile>();
        var movePatterns = chessmanSO.Name.movePatterns;

        foreach (var movePattern in movePatterns)
        {
            int patternPosX = teamColor == TeamColor.white ? movePattern.posX : -movePattern.posX;
            int patternPosZ = teamColor == TeamColor.white ? movePattern.posZ : -movePattern.posZ;
            Tile availableTile = boardManager.ReturnTile(position.posX + patternPosX, position.posZ);

            if (availableTile != null)
            {
                if (availableTile.Chessman == null)
                {
                    availableTiles.Add(availableTile);
                }
            }
        }

        foreach (Tile combatTile in ReturnCombatTiles()) 
        {
            if(combatTile == null)
            {
                break;
            }

            if(combatTile.Chessman != null && teamColor != combatTile.Chessman.GetTeamColor())
            {
                availableTiles.Add(combatTile);
            }
        }

        if (IsOnStartingPosition())
        {
            boardManager.ReturnTile(position.posX, position.posZ).AddBonucMoveableChessman(this);

            if(ReturnBonusTile().Chessman == null)
            {
                availableTiles.Add(ReturnBonusTile());
            }

            usedBonusMove = true;
            return availableTiles;
        }

        usedBonusMove = false;

        foreach (Tile beatEnPassantTile in ReturnBeatEnPassantTiles())
        {
            if(beatEnPassantTile == null)
            {
                break;
            }

            availableTiles.Add(beatEnPassantTile);
        }

        return availableTiles;
    }
}
