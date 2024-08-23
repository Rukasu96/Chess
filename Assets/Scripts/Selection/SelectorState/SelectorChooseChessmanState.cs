using System;
using UnityEngine;

public class SelectorChooseChessmanState : SelectorBaseState
{
    private int highlightedChessmanIndex;
    private int hoveredChessmanIndex;
    private Chessman hoveredChessman;

    private ISelectable selectable;

    public SelectorChooseChessmanState(BoardManager board, IHoveredSelector hoverSelector) : base(board,hoverSelector)
    {
    }

    public override void EnterState()
    {
        highlightedChessmanIndex = 0;
        hoveredChessmanIndex = 0;
    }

    public override Type UpdateState()
    {
        HoverObject();
        if (Input.GetMouseButtonDown(0) && selectable != null)
        {
            SelectChessman(hoveredChessman);
            selectable.OnHover();
            return typeof(SelectorChooseTileState);
        }
        return null;
    }

    public override void HoverObject()
    {
        if (hoveredSelector.IsObjectHovered(out var LocalSelectable) && selectable != LocalSelectable)
        {
            hoveredChessman = hoveredSelector.GetHoveredObject().GetComponent<Chessman>();

            if (!boardManager.CheckChessmanTeamColor(hoveredChessman))
            {
                if(selectable  != null)
                {
                    selectable.OnNotHover();
                    selectable = null;
                }
                hoveredChessman = null;
                return;
            }

            hoveredChessmanIndex = boardManager.GetChessmanIndex(hoveredChessman);

            if (selectable != null && hoveredChessmanIndex != highlightedChessmanIndex)
            {
                selectable.OnNotHover();
            }

            selectable = LocalSelectable;
            highlightedChessmanIndex = hoveredChessmanIndex;
            selectable.OnHover();
        }
        else if (selectable != null && LocalSelectable == null)
        {
            selectable.OnNotHover();
            selectable = null;
        }
    }
}
