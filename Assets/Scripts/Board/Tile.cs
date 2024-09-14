using UnityEngine;

public class Tile : MonoBehaviour
{
    private Renderer renderer;
    private Color tileColor;
    private IBonusMoveable bonusMoveableChessman;
    private TeamColor teamColor;
    private bool isEnPassantTile;

    public Chessman Chessman;
    public PositionOnGrid PositionOnGrid;

    public bool IsEnPassantTile => isEnPassantTile;
    public TeamColor TeamColor => teamColor;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        tileColor = renderer.materials[0].color;
    }

    public void UpdateEnPassantStatus()
    {
        isEnPassantTile = !isEnPassantTile;
    }

    public void SetTeamColor(TeamColor teamColor)
    {
        this.teamColor = teamColor;
    }

    public void ChangeColor(Color color)
    {
        renderer.material.color = color;
    }

    public void BackToDefaultColor()
    {
        renderer.material.color = tileColor;
    }

    public bool HasBonusMoveableChessman()
    {
        if(bonusMoveableChessman == null)
        {
            return false;
        }
        return true;
    }
}
