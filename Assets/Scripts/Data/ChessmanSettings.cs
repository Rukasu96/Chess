using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Name
{
    public GameObject ChessmanPrefab;
    public List<PositionOnGrid> MovePatterns;
    public List<PositionOnGrid> CombatPatterns;
    public bool IsMovingFullHorizontalAndVertical;
    public bool IsMovingFullSidelong;
    public bool IsKing;
}

[CreateAssetMenu(fileName = "Data", menuName = "ChessmanSettings")]
public class ChessmanSettings : ScriptableObject
{
    public Name Name;
}
