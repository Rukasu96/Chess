using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    private Board board;
    private bool rotateFinished;
    
    private void Awake()
    {
        board = GetComponent<Board>();
    }

    public int GetChessmanIndex(Chessman selectedChessman)
    {
        return board.GetChessmanIndex(selectedChessman, turnManager.ActivePlayer);
    }

    public bool CheckChessmanTeamColor(Chessman hoveredChessman)
    {
        return hoveredChessman.GetTeamColor() == turnManager.ActivePlayer;
    }

    public void ChangeBoardTileColor(List<Tile> availableTiles)
    {
        board.ChangeTileColor(availableTiles, turnManager.ActivePlayer);
    }

    public void UpdateBoard(Tile selectedTile, Chessman selectedChessman)
    {
        board.UpdateBoard(selectedTile, selectedChessman);
    }

    public void RotateBoard() 
    {
        rotateFinished = false;
        float YRotationValue = board.BoardParent.transform.eulerAngles.y + 180;
        board.BoardParent.transform.DORotate(new Vector3(0f,YRotationValue,0f), 1f).OnComplete(() => rotateFinished = true);
    }
    
    public bool IsRotationFinish()
    {
        return rotateFinished;
    }

    public Tile ReturnTile(int posX, int posZ)
    {
        return board.ReturnTile(posX, posZ);
    }
}
