using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ChessmanSettings")]
public class ChessmanSetting : ScriptableObject
{
    public GameObject ChessmanPrefab;
    public List<PositionOnGrid> MovePatterns;
    public List<PositionOnGrid> CombatPatterns;
    public bool IsMovingFullHorizontalAndVertical;
    public bool IsMovingFullSidelong;
    public bool IsKing;
}
