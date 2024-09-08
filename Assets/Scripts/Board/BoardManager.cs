using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    private Board board;
    
    private void Awake()
    {
        board = GetComponent<Board>();
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

    public Tile ReturnTile(int posX, int posZ)
    {
        return board.ReturnTile(posX, posZ);
    }
}
