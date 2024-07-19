using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Name
{
    public GameObject chessmanPrefab;
    public List<PositionOnGrid> movePatterns;
}

[CreateAssetMenu(fileName = "Data", menuName = "ChessmanSettings")]
public class ChessmanSettings : ScriptableObject
{
    public Name Name;
}
