using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Name
{
    public GameObject chessmanPrefab;
    public List<PositionOnGrid> movePatterns;
    public List<PositionOnGrid> combatPatterns;
    public bool IsMovingFullHorizontalAndVertical;
    public bool IsMovingFullSidelong;
    public bool IsKing;
}

[CreateAssetMenu(fileName = "Data", menuName = "ChessmanSettings")]
public class ChessmanSettings : ScriptableObject
{
    public Name Name;
}
