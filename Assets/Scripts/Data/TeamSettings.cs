using System.Collections.Generic;
using UnityEngine;

public enum TeamColor
{
    white,
    black
}
[System.Serializable]
public struct TeamChessman
{
    public ChessmanSettings type;
    public List<PositionOnGrid> startingPosition;
    public float YrotationDegrees;
}
[CreateAssetMenu(fileName = "TeamSettings", menuName = "TeamData")]
public class TeamSettings : ScriptableObject
{
    public TeamColor teamColor;
    public List<TeamChessman> chessmanList;
    public Dictionary<Chessman, int> ChessmanDictionary = new Dictionary<Chessman, int>();
}
