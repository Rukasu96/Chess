using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorChooseTileState : SelectorBaseState
{
    private ISelectable selectable;
    private Tile hoveredTile;

    public SelectorChooseTileState(BoardManager boardManager, IHoveredSelector hoverSelector) : base(boardManager, hoverSelector)
    {
    }

    public override void EnterState()
    {
    }

    public override Type UpdateState()
    {
        HoverObject();
        if (Input.GetMouseButtonDown(0) && hoveredTile != null)
        {
            SelectTileToMove();
            return typeof(SelectorChangingPlayerState);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            BackToDefault();
            return typeof(SelectorChooseChessmanState);
        }
        return null;
    }

    public override void HoverObject()
    {
        if (hoveredSelector.IsObjectHovered(out ISelectable localSelectable) && selectable != localSelectable)
        {
            selectable = hoveredSelector.GetHoveredObject().GetComponent<ISelectable>();
            hoveredTile = hoveredSelector.GetHoveredObject().GetComponent<Tile>();

            if (hoveredTile)
            {
                selectable = localSelectable;
                selectable.OnHover();
            }
        }
        else if (selectable != null && localSelectable == null)
        {
            selectable.OnNotHover();
            selectable = null;
            hoveredTile = null;
        }
    }

    private void SelectTileToMove()
    {
        SelectTile(hoveredTile);
        selectable.OnNotHover();
    }
}
