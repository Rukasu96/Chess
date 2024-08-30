using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Renderer renderer;
    private Color defaultColor;
    private IBonusMoveable bonusMoveableChessman;
    public Chessman Chessman;
    public PositionOnGrid PositionOnGrid;
    public TeamColor teamColor;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
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
    public void AddBonucMoveableChessman(Chessman bonusMoveable)
    {
        if(bonusMoveable.GetComponent<IBonusMoveable>() != null)
        {
            bonusMoveableChessman = bonusMoveable.GetComponent<IBonusMoveable>();
            teamColor = bonusMoveable.GetTeamColor();
        }
    }
    public void RemovebonusMoveableChessman()
    {
        bonusMoveableChessman = null;
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
