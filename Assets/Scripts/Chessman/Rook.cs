using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Rook : Chessman
{
    private PositionOnGrid startingPosition;

    private void Start()
    {
        startingPosition = position;
    }

    public override List<Tile> GetAvailableTilesToMove()
    {
        return ReturnAvailableHorizontalAndVerticalTiles();
    }

    private List<Tile> ReturnAvailableHorizontalAndVerticalTiles()
    {
        List<Tile> HorizontalAndVerticalTiles = new List<Tile>();
        var movePatterns = chessmanSO.Name.movePatterns;
        Tile availableTile;
        int modifierX = 1;
        int modifierZ = 0;
        int movePatternValue = movePatterns[0].posX;

        for (int i = 0; i < 4; i++)
        {
            for (int x = 0; x < movePatternValue; x++)
            {
                if (i == 0 || i == 2)
                {
                    availableTile = boardManager.ReturnTile(position.posX + modifierX, position.posZ + modifierZ);
                }
                else
                {
                    availableTile = boardManager.ReturnTile(position.posX - modifierX, position.posZ - modifierZ);
                }

                if (availableTile == null)
                {
                    break;
                }

                if (availableTile.Chessman != null)
                {
                    if (availableTile.Chessman.GetTeamColor() != teamColor)
                    {
                        HorizontalAndVerticalTiles.Add(availableTile);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                if (i >= 2)
                {
                    modifierZ++;
                }
                else
                {
                    modifierX++;
                }

                HorizontalAndVerticalTiles.Add(availableTile);
            }

            if (i == 1)
            {
                modifierX = 0;
                modifierZ = 1;
            }
            else if (i >= 1)
            {
                modifierZ = 1;
                movePatternValue = movePatterns[0].posZ;
            }
            else
            {
                modifierX = 1;
            }
        }
        return HorizontalAndVerticalTiles;
    }
}
