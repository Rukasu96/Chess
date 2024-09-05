using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Renderer renderer;
    private Color defaultColor;
    private IBonusMoveable bonusMoveableChessman;
    private bool isEnPassantTile;
    private TeamColor teamColor;
    public Chessman Chessman;
    public PositionOnGrid PositionOnGrid;

    public bool IsEnPassantTile => isEnPassantTile;
    public TeamColor TeamColor => teamColor;
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void UpdateEnPassantStatus()
    {
        isEnPassantTile = !isEnPassantTile;
    }

    public void SetTeamColor(TeamColor teamColor)
    {
        this.teamColor = teamColor;
    }
    
    public void SetColor(Color color)
    {
        defaultColor = color;
        renderer.material.color = defaultColor;
    }

    public void ChangeColor(Color color)
    {
        renderer.material.color = color;
    }

    public void BackToDefaultColor()
    {
        renderer.material.color = defaultColor;
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
