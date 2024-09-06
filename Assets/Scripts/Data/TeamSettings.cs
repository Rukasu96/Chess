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
    public ChessmanSettings Type;
    public List<PositionOnGrid> StartingPosition;
    public float YrotationDegrees;
}

[CreateAssetMenu(fileName = "TeamSettings", menuName = "TeamData")]
public class TeamSettings : ScriptableObject
{
    public TeamColor TeamColor;
    public List<TeamChessman> ChessmanList;
    public Dictionary<Chessman, int> ChessmanDictionary = new Dictionary<Chessman, int>();
}
