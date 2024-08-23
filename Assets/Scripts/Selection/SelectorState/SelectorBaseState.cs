using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectorBaseState
{
    public event Action<Chessman> OnSelectChessman;
    public event Action<Tile> OnSelectTile;
    public event Action OnBackToDefaultState;

    protected BoardManager boardManager;

    protected IHoveredSelector hoveredSelector;

    protected SelectorBaseState(BoardManager board, IHoveredSelector hoveredSelector)
    {
        boardManager = board;
        this.hoveredSelector = hoveredSelector;
    }
    protected void SelectChessman(Chessman selectedChessman)
    {
        OnSelectChessman?.Invoke(selectedChessman);
    }
    protected void SelectTile(Tile selectedTile)
    {
        OnSelectTile?.Invoke(selectedTile);
    }
    protected void BackToDefault()
    {
        OnBackToDefaultState?.Invoke();
    }

    public abstract void EnterState();
    public abstract Type UpdateState();
    public abstract void HoverObject();
}
