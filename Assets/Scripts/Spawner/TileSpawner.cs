using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public List<TileSetting> TileSetting;

    ITileFactory tileFactory = new TileFactory();

    private Tile SpawnWhiteTiles()
    {
        return tileFactory.Create(TileSetting[0]);
    }

    private Tile SpawnBlackTiles()
    {
        return tileFactory.Create(TileSetting[1]);
    }

    public Tile[] SpawnChessBoard(int width, int height)
    {
        Tile[] boardTiles = new Tile[width * height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if ((j + i) % 2 == 0)
                {
                    boardTiles[j + i * width] = SpawnWhiteTiles();
                }
                else
                {
                    boardTiles[j + i * width] = SpawnBlackTiles();
                }
            }
        }

        return boardTiles;
    }
}
