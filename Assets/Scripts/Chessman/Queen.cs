using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman
{
    private List<Tile> crossedHorizontalAndVerticalTilesList;

    public override List<Tile> GetAvailableTilesToMove()
    {
        crossedHorizontalAndVerticalTilesList = new List<Tile>();
        AddAvailableHorizontalAndVerticalTilesToList();
        AddCrossedTilesToList();

        return crossedHorizontalAndVerticalTilesList;
    }

    private void AddAvailableHorizontalAndVerticalTilesToList()
    {
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
                        crossedHorizontalAndVerticalTilesList.Add(availableTile);
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

                crossedHorizontalAndVerticalTilesList.Add(availableTile);
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
    }

    private void AddCrossedTilesToList()
    {
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
                        crossedHorizontalAndVerticalTilesList.Add(availableTile);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                crossedHorizontalAndVerticalTilesList.Add(availableTile);
            }
        }
    }
}
