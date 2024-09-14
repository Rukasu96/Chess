using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorCheckState : SelectorBaseState
{
    //private ChessmanDictionaryManager chessmanDictionary;
    //private int highlightedChessmanIndex;
    //private int hoveredChessmanIndex;
    private Chessman hoveredChessman;

    private ISelectable selectable;

    public SelectorCheckState(BoardManager board, IHoveredSelector hoverSelector) : base(board, hoverSelector)
    {
    }

    public override void EnterState()
    {
        //highlightedChessmanIndex = 0;
        //hoveredChessmanIndex = 0;
        if (selectable != null)
        {
            selectable.SelectionUpdate();
        }
    }

    public override void HoverObject()
    {
        if (hoveredSelector.IsObjectHovered(out var LocalSelectable) && selectable != LocalSelectable)
        {
            hoveredChessman = hoveredSelector.GetHoveredObject().GetComponent<Chessman>();
            IKingable kingComponent = hoveredChessman.GetComponent<IKingable>(); 

            if (!boardManager.CheckChessmanTeamColor(hoveredChessman) || kingComponent == null)
            {
                if (selectable != null && !selectable.IsSelected())
                {
                    selectable.OnNotHover();
                    selectable = null;
                }
                hoveredChessman = null;
                return;
            }

            //hoveredChessmanIndex = chessmanDictionary.ReturnChessmanIndex(hoveredChessman, hoveredChessman.GetTeamColor());

            if ((selectable != null && !selectable.IsSelected()) || kingComponent == null)
            {
                selectable.OnNotHover();
            }

            selectable = LocalSelectable;
            //highlightedChessmanIndex = hoveredChessmanIndex;
            selectable.OnHover();
        }
        else if (selectable != null && LocalSelectable == null && !selectable.IsSelected())
        {
            selectable.OnNotHover();
            selectable = null;
        }
    }

    public override Type UpdateState()
    {
        HoverObject();
        if (Input.GetMouseButtonDown(0) && selectable != null)
        {
            SelectChessman(hoveredChessman);
            selectable.OnHover();
            selectable.SelectionUpdate();
            return typeof(SelectorChooseTileState);
        }
        return null;
    }
}
