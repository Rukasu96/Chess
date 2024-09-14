using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardSpawner boardSpawner;

    private Tile[][] chessBoard;

    private void Start()
    {
        boardSpawner.SpawnBoard();
        chessBoard = boardSpawner.ReturnBoardtiles();
    }
    
    public Tile ReturnTile(int posX, int posZ)
    {
        if (chessBoard.Length - 1 < posZ || posZ < 0)
        {
            return null;
        }
        
        foreach (var tiles in chessBoard)
        {
            if (tiles.Length - 1 < posX || posX < 0)
            {
                return null;
            }
        }

        return chessBoard[posX][posZ];
    }

    public void ChangeTileColor(List<Tile> availableTiles, TeamColor activePlayer)
    {

        foreach (Tile tile in availableTiles)
        {
            Tile tileWithChessmanBonusMove;

            if (activePlayer == TeamColor.white)
            {
                tileWithChessmanBonusMove = ReturnTile(tile.PositionOnGrid.posX + 1, tile.PositionOnGrid.posZ);
            }
            else
            {
                tileWithChessmanBonusMove = ReturnTile(tile.PositionOnGrid.posX - 1, tile.PositionOnGrid.posZ);
            }

            tile.tag = tile.tag != "Selectable" ? "Selectable" : "Untagged";

            if (tile.CompareTag("Selectable"))
            {
                tile.ChangeColor(Color.green);

                if (tile.Chessman is IKingable && tile.Chessman.GetTeamColor() == activePlayer)
                {
                    tile.ChangeColor(Color.green);
                }
                else if (tile.Chessman != null || (tile.IsEnPassantTile && tile.TeamColor != activePlayer))
                {
                    tile.ChangeColor(Color.yellow);
                }
                else if (tileWithChessmanBonusMove.HasBonusMoveableChessman())
                {
                    if(activePlayer != tileWithChessmanBonusMove.TeamColor)
                    {
                        tile.ChangeColor(Color.yellow);
                    }
                }
            }
            else
                tile.BackToDefaultColor();
        }
    }

    public void UpdateBoard(Tile selectedTile, Chessman selectedChessman)
    {
        Tile previousTile = ReturnTile(selectedChessman.GetPosition().posX, selectedChessman.GetPosition().posZ);
        Chessman enemyChessman = selectedTile.Chessman;

        if (enemyChessman != null)
        {
            selectedTile.Chessman = null;
            Destroy(enemyChessman.gameObject);
        }
        else if (selectedTile.IsEnPassantTile && selectedTile.TeamColor != selectedChessman.GetTeamColor())
        {
            Tile EnPassantTile;
            if(selectedChessman.GetTeamColor() == TeamColor.white)
            {
                EnPassantTile = ReturnTile(selectedTile.PositionOnGrid.posX - 1, selectedTile.PositionOnGrid.posZ);
            }
            else
            {
                EnPassantTile = ReturnTile(selectedTile.PositionOnGrid.posX + 1, selectedTile.PositionOnGrid.posZ);
            }

            enemyChessman = EnPassantTile.Chessman;
            enemyChessman.GetComponent<IEnPassantable>().SetEnPassantTileBackToDefault();
            EnPassantTile.Chessman = null;
            Destroy(enemyChessman.gameObject);
        }

        previousTile.BackToDefaultColor();
        previousTile.Chessman = null;
        selectedTile.Chessman = selectedChessman;
    }
}