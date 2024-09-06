using System;

public class SelectorChangingPlayerState : SelectorBaseState
{
    public SelectorChangingPlayerState(BoardManager board, IHoveredSelector hoveredSelector) : base(board, hoveredSelector)
    {
    }

    public override void EnterState()
    {
        //Doda� jaki� panel z nazw� kto teraz gra
    }

    public override void HoverObject()
    {
    }

    public override Type UpdateState()
    {
        if(boardManager.IsRotationFinish())
        {
            return typeof(SelectorChooseChessmanState);
        }
        return null;
    }
}
