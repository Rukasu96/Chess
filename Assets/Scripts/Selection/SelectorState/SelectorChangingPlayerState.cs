using System;

public class SelectorChangingPlayerState : SelectorBaseState
{
    private CameraRotator cameraRotator;

    public SelectorChangingPlayerState(BoardManager board, IHoveredSelector hoveredSelector, CameraRotator cameraRotator) : base(board, hoveredSelector)
    {
        this.cameraRotator = cameraRotator;
    }

    public override void EnterState()
    {
    }

    public override void HoverObject()
    {
    }

    public override Type UpdateState()
    {
        if(cameraRotator.IsRotationFinish())
        {
            return typeof(SelectorChooseChessmanState);
        }
        return null;
    }
}
