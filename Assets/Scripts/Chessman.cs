using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    [SerializeField] private ChessmanSettings chessmanSO;
    private PositionOnGrid position;
    private TeamColor teamColor;
    public void PositionUpdate(int posX, int posZ)
    {
        position.posX = posX;
        position.posZ = posZ;
    }
    public void MoveToNewTile(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
    public PositionOnGrid GetPosition()
    {
        return position;
    }
    public List<PositionOnGrid> GetMovePatterns()
    {
        return chessmanSO.Name.movePatterns;
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
