using System;
using UnityEngine;

public class SelectorChooseChessmanState : SelectorBaseState
{
    private ChessmanDictionaryManager chessmanDictionary;
    private int highlightedChessmanIndex;
    private int hoveredChessmanIndex;
    private Chessman hoveredChessman;

    private ISelectable selectable;

    public SelectorChooseChessmanState(BoardManager board, ChessmanDictionaryManager chessmanDictionaryManager ,IHoveredSelector hoverSelector) : base(board,hoverSelector)
    {
        chessmanDictionary = chessmanDictionaryManager;
    }

    public override void EnterState()
    {
        highlightedChessmanIndex = 0;
        hoveredChessmanIndex = 0;
        if(selectable != null)
        {
            selectable.SelectionUpdate();
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

    public override void HoverObject()
    {
        if (hoveredSelector.IsObjectHovered(out var LocalSelectable) && selectable != LocalSelectable)
        {
            hoveredChessman = hoveredSelector.GetHoveredObject().GetComponent<Chessman>();

            if (!boardManager.CheckChessmanTeamColor(hoveredChessman))
            {
                if(selectable  != null && !selectable.IsSelected())
                {
                    selectable.OnNotHover();
                    selectable = null;
                }
                hoveredChessman = null;
                return;
            }

            hoveredChessmanIndex = chessmanDictionary.ReturnChessmanIndex(hoveredChessman, hoveredChessman.GetTeamColor());

            if (selectable != null && hoveredChessmanIndex != highlightedChessmanIndex && !selectable.IsSelected())
            {
                selectable.OnNotHover();
            }

            selectable = LocalSelectable;
            highlightedChessmanIndex = hoveredChessmanIndex;
            selectable.OnHover();
        }
        else if (selectable != null && LocalSelectable == null && !selectable.IsSelected())
        {
            selectable.OnNotHover();
            selectable = null;
        }
    }
}
