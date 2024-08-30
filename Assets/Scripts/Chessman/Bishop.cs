using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman
{
    public override List<Tile> GetAvailableTilesToMove()
    {
        return ReturnAvailableAllCrossTiles();
    }

    private List<Tile> ReturnAvailableAllCrossTiles()
    {
        List<Tile> CrossTiles = new List<Tile>();
        Tile availableTile;

        for (int i = 0; i < 4; i++)
        {
            int modifierX = 0;
            int modifierZ = 0;

            for (int x = 0; x < chessmanSO.Name.movePatterns[0].posX; x++)
            {
                switch (i)
                {
                    case 0:
                        modifierX++;
                        modifierZ++;
                        break;
                    case 1:
                        modifierX--;
                        modifierZ++;
                        break;
                    case 2:
                        modifierX++;
                        modifierZ--;
                        break;
                    case 3:
                        modifierX--;
                        modifierZ--;
                        break;
                    default:
                        break;
                };

                availableTile = boardManager.ReturnTile(position.posX + modifierX, position.posZ + modifierZ);

                if (availableTile == null)
                {
                    break;
                }

                if (availableTile.Chessman != null)
                {
                    if (availableTile.Chessman.GetTeamColor() != teamColor)
                    {
                        CrossTiles.Add(availableTile);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                CrossTiles.Add(availableTile);
            }
        }
        return CrossTiles;
    }
}
