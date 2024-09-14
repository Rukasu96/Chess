using UnityEngine;

public class Pawn : Chessman, IBonusMoveable, IEnPassantable, IPromotable
{
    [SerializeField] PositionOnGrid bonusMovePattern;
    private Tile enPassantTile;
    private bool hasBonusMove = true;

    public bool HasBonusMove()
    {
        return hasBonusMove;
    }

    public PositionOnGrid ReturnBonusMovePattern()
    {
        return bonusMovePattern;
    }

    public void UpdateSatusBonusMove()
    {
        hasBonusMove = false;
    }
    
    public void UpdateEnPassantTileStatus()
    {
        if (teamColor == TeamColor.white)
        {
            enPassantTile = boardManager.ReturnTile(position.posX + 1, position.posZ);
        }
        else
        {
            enPassantTile = boardManager.ReturnTile(position.posX - 1, position.posZ);
        }

        enPassantTile.UpdateEnPassantStatus();
        enPassantTile.SetTeamColor(teamColor);
    }

    public void SetEnPassantTileBackToDefault()
    {
        enPassantTile.UpdateEnPassantStatus();
    }

    public void Promotion(ChessmanSetting setting)
    {
        Initialize(setting);
        GetComponent<MeshFilter>().mesh = setting.ChessmanPrefab.GetComponent<MeshFilter>().mesh;
    }
}
