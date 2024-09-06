using System;

public class SelectorChangingPlayerState : SelectorBaseState
{
    public SelectorChangingPlayerState(BoardManager board, IHoveredSelector hoveredSelector) : base(board, hoveredSelector)
    {
    }

    public override void EnterState()
    {
        //Dodaæ jakiœ panel z nazw¹ kto teraz gra
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
