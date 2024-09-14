using System;

public class SelectorChangingPlayerState : SelectorBaseState
{
    private GameManager gameManager;
    private CameraRotator cameraRotator;
    private bool isCheck;

    public SelectorChangingPlayerState(BoardManager board, IHoveredSelector hoveredSelector, CameraRotator cameraRotator, GameManager gameManager) : base(board, hoveredSelector)
    {
        this.cameraRotator = cameraRotator;
        this.gameManager = gameManager;
    }

    public override void EnterState()
    {
        isCheck = gameManager.IsCheck();
    }

    public override void HoverObject()
    {
    }

    public override Type UpdateState()
    {
        if(cameraRotator.IsRotationFinish())
        {
            if(isCheck)
            {
                return typeof(SelectorCheckState);
            }

            return typeof(SelectorChooseChessmanState);
        }

        return null;
    }
}
