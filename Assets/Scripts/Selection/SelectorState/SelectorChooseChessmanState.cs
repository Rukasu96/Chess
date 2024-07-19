using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorChooseChessmanState : SelectorBaseState
{
    private int highlightedChessmanIndex = 0;
    private int hoveredChessmanIndex = 0;
    private Chessman hoveredChessman;
    public override void EnterState(SelectorStateManager selectorManager)
    {
        board = selectorManager.Board;
        rayProvider = selectorManager.RayProvider;
        hoveredSelector = selectorManager.HoveredSelector;
        highlightedChessmanIndex = 0;
        highlightedChessmanIndex = 0;
    }
    public override void UpdateState(SelectorStateManager selectorManager)
    {
        HoverObject();
        if(Input.GetMouseButtonDown(0) && selectable != null)
        {
            selectorManager.SelectedChessman = hoveredChessman;
            selectable.OnHover();
            selectorManager.SwitchState(selectorManager.ChooseTileState);
        }
    }
    public override void HoverObject()
    {
        if (hoveredSelector.IsObjectHovered(rayProvider.CreateRay(), out var LocalSelectable) && selectable != LocalSelectable)
        {
            hoveredChessman = hoveredSelector.GetHoveredObject().GetComponent<Chessman>();
            hoveredChessmanIndex = board.GetChessmanIndex(hoveredChessman);

            if (selectable != null && hoveredChessmanIndex != highlightedChessmanIndex)
            {
                selectable.OnNotHover();
            }

            selectable = LocalSelectable;
            highlightedChessmanIndex = board.GetChessmanIndex(hoveredChessman);
            selectable.OnHover();
        }
        else if (selectable != null && LocalSelectable == null)
        {
            selectable.OnNotHover();
            selectable = null;
        }
    }
}
