using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorChooseTileState : SelectorBaseState
{
    private Chessman selectedChessman;
    public override void EnterState(SelectorStateManager selectorManager)
    {
        board = selectorManager.Board;
        rayProvider = selectorManager.RayProvider;
        hoveredSelector = selectorManager.HoveredSelector;
        selectedChessman = selectorManager.SelectedChessman;
        board.ChangeTileColor(selectedChessman, Color.green);
    }
    public override void UpdateState(SelectorStateManager selectorManager)
    {
        HoverObject();
        if (Input.GetMouseButtonDown(0))
        {
            SelectTileToMove();
            selectorManager.SwitchState(selectorManager.ChooseChessmanState);
        }
    }
    public override void HoverObject()
    {
        if (hoveredSelector.IsObjectHovered(rayProvider.CreateRay(), out ISelectable localSelectable) && selectable != localSelectable)
        {
            if (hoveredSelector.GetHoveredObject().GetComponent<Tile>())
            {
                selectable = localSelectable;
                selectable.OnHover();
            }
        }
        else if (selectable != null && localSelectable == null)
        {
            selectable.OnNotHover();
            selectable = null;
        }
    }
    private void SelectTileToMove()
    {
        Tile tile = hoveredSelector.GetHoveredObject().GetComponent<Tile>();
        board.ChangeTileColor(selectedChessman, Color.white);
        board.UpdateBoard(tile, selectedChessman);
        selectable.OnNotHover();
    }
}
