using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CheckTileStatusController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private List<Tile> whiteCheckTiles = new List<Tile>();
    private List<Tile> blackCheckTiles = new List<Tile>();

    public List<Tile> WhiteCheckTiles => whiteCheckTiles;
    public List<Tile> BlackCheckTiles => blackCheckTiles;

    public bool IsTileInList(Tile tile, TeamColor teamColor)
    {
        if(teamColor == TeamColor.white)
        {
            return blackCheckTiles.Contains(tile);
        }
        
        return whiteCheckTiles.Contains(tile);
    }

    public void AddTileToCheckList(List<Tile> checkTiles, Chessman chessman)
    {
        foreach (Tile tile in checkTiles)
        {
            if (chessman.GetTeamColor() == TeamColor.white)
            {
                whiteCheckTiles.Add(tile);
            }
            else
            {
                blackCheckTiles.Add(tile);
            }
        }

        gameManager.TryCheckMate();
    }

    public void RemoveOldCheckTiles(List<Tile> oldCheckTiles, Chessman chessman)
    {
        foreach (Tile tile in oldCheckTiles)
        {
            if(chessman.GetTeamColor() == TeamColor.white)
            {
                whiteCheckTiles.Remove(tile);
            }
            else
            {
                blackCheckTiles.Remove(tile);
            }
        }
    }

}
