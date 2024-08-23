using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    [SerializeField] protected ChessmanSettings chessmanSO;
    [SerializeField] protected BoardManager boardManager;
    protected PositionOnGrid position;
    protected TeamColor teamColor;

    private void Start()
    {
        boardManager = FindAnyObjectByType<BoardManager>();
    }

    public virtual List<Tile> GetAvailableTilesToMove()
    {
        List<Tile> availableTiles = new List<Tile>();
        var movePatterns = chessmanSO.Name.movePatterns;

        foreach (var movePattern in movePatterns)
        {
            int patternPosX = teamColor == TeamColor.white ? movePattern.posX : -movePattern.posX;
            int patternPosZ = teamColor == TeamColor.white ? movePattern.posZ : -movePattern.posZ;
            Tile availableTile = boardManager.ReturnTile(position.posX + patternPosX, position.posZ + patternPosZ);

            if (availableTile != null)
            {
                if (availableTile.Chessman == null || availableTile.Chessman.GetTeamColor() != teamColor)
                {
                    availableTiles.Add(availableTile);
                }
            }
        }
        return availableTiles;
    }

    public void UpdatePositionOnGrid(int posX, int posZ)
    {
        position.posX = posX;
        position.posZ = posZ;
    }
    public void SetChessmanOnTile(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public PositionOnGrid GetPosition()
    {
        return position;
    }
    
    public TeamColor GetTeamColor()
    {
        return teamColor;
    }
    public void SetTeamColor(TeamColor teamColor)
    {
        this.teamColor = teamColor;
    }
}
