using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private TurnManager turnManager;
    List<Tile> availableTiles = new List<Tile>();
    private Chessman currentChessman;

    public void BackToDefault()
    {
        currentChessman = null;
        boardManager.ChangeBoardTileColor(availableTiles);
        availableTiles.Clear();
    }
    
    public void SetCurrentChessman(Chessman selectedChessman)
    {
        if(selectedChessman == null)
        {
            return;
        }

        currentChessman = selectedChessman;
        SetTilesAvailableToMove();
    }

    public void MoveChessman(Tile selectedTile)
    {
        //Przemieszczenie figury na tile'a
        Vector3 destinationTile = new Vector3(selectedTile.transform.position.x, 0.5f, selectedTile.transform.position.z);
        boardManager.UpdateBoard(selectedTile, currentChessman);
        currentChessman.UpdatePositionOnGrid(selectedTile.PositionOnGrid.posX, selectedTile.PositionOnGrid.posZ);
        currentChessman.SetChessmanOnTile(destinationTile);

        boardManager.ChangeBoardTileColor(availableTiles);
        
        availableTiles.Clear();
    }
    
    private void SetTilesAvailableToMove()
    {
        availableTiles = currentChessman.GetAvailableTilesToMove();
        boardManager.ChangeBoardTileColor(availableTiles);
    }
    //Zbijanie figur
    //roszady

}
