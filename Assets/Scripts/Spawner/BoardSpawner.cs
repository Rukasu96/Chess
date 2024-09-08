using UnityEngine;

public class BoardSpawner : MonoBehaviour
{
    [SerializeField] private BoardSetting boardSetting;

    private ChessmanSpawner chessmanSpawner;
    private TileSpawner tileSpawner;
    private GridPlacer gridPlacer;

    private Tile[][] boardTiles;

    private void Awake()
    {
        chessmanSpawner = GetComponent<ChessmanSpawner>();
        tileSpawner = GetComponent<TileSpawner>();
        gridPlacer = GetComponent<GridPlacer>();
    }

    private Tile[][] CreateBoard()
    {
        Tile[][] chessBoard = new Tile[boardSetting.Width][];

        for (int i = 0; i < boardSetting.Width; i++)
        {
            chessBoard[i] = new Tile[boardSetting.Height];
        }

        return chessBoard;
    }

    public void SpawnBoard()
    {
        boardTiles = CreateBoard();
        Tile[] spawnedTiles = tileSpawner.SpawnChessBoard(boardSetting.Width, boardSetting.Height);
        gridPlacer.SetGridPosition(boardTiles, spawnedTiles, boardSetting.TileOffsetPosition);
        chessmanSpawner.SpawnChessmans(boardSetting, boardTiles);
    }

    public Tile[][] ReturnBoardtiles()
    {
        return boardTiles;
    }
}
