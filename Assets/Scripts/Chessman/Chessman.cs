using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    [SerializeField] protected ChessmanSettings chessmanSO;
    [SerializeField] protected BoardManager boardManager;
    protected PositionOnGrid position;
    protected TeamColor teamColor;

    public ChessmanSettings ChessmanSettings => chessmanSO;

    private void Start()
    {
        boardManager = FindAnyObjectByType<BoardManager>();
    }

    public void UpdatePositionOnGrid(int posX, int posZ)
    {
        position.posX = posX;
        position.posZ = posZ;
    }

    public void SetChessmanOnTile(Tile destinationTile)
    {
        Vector3 destination = new Vector3(destinationTile.transform.position.x, 0.5f, destinationTile.transform.position.z);
        transform.position = destination;
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

    public List<PositionOnGrid> ReturnMovePatterns()
    {
        return chessmanSO.Name.movePatterns;
    }
    public List<PositionOnGrid> ReturnCombatPatterns()
    {
        return chessmanSO.Name.combatPatterns;
    }
}
