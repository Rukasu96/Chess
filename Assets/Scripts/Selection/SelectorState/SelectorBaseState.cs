using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectorBaseState
{
    protected Board board;

    protected ISelectable selectable;
    protected IRayProvider rayProvider;
    protected IHoveredSelector hoveredSelector;
    public abstract void EnterState(SelectorStateManager selectorManager);
    public abstract void UpdateState(SelectorStateManager selectorManager);
    public abstract void HoverObject();
}
