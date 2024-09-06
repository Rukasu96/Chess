using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private BoardSettings settings;
    private Tile[][] chessBoard;
    private GameObject boardParent;
    private List<Chessman> chessmanList = new List<Chessman>();
    public TeamSettings[] TeamSettings => settings.TeamSettings;
    public GameObject BoardParent => boardParent;

    private void Awake()
    {
        SetupBoard();
    }
    private void SetupBoard()
    {
        boardParent = new GameObject("BoardParent");
        boardParent.transform.position = new Vector3(settings.BoardPositionX, 0f, settings.BoardPositionZ); 

        chessBoard = new Tile[settings.Width][];

        for (int i = 0; i < settings.Width; i++)
        {
            chessBoard[i] = new Tile[settings.Height];
        }

        CreateTiles();
    }

    private void CreateTiles()
    {
        float tileBoardCenterOffsetX = (settings.Width + settings.Height) / (settings.Width / 2) - settings.BoardPositionX;
        float tileBoardCenterOffsetZ = (settings.Width + settings.Height) / (settings.Height / 2) - settings.BoardPositionZ;
        Color currentColor;
        bool oddRow = false;

        for (int i = 0; i < settings.Width; i++)
        {
            currentColor = oddRow ? Color.white : Color.black;
            
            for (int j = 0; j < settings.Height; j++)
            {
                Tile tileClone = Instantiate(settings.TilePrefab, new Vector3(i * settings.TileOffsetPosition - tileBoardCenterOffsetX, 0, j * settings.TileOffsetPosition - tileBoardCenterOffsetZ), Quaternion.identity);
                tileClone.transform.SetParent(boardParent.transform);
                tileClone.SetColor(currentColor);

                currentColor = currentColor == Color.white ? Color.black : Color.white;
                tileClone.PositionOnGrid.posX = i;
                tileClone.PositionOnGrid.posZ = j;
                chessBoard[i][j] = tileClone;
            }
            oddRow = !oddRow;
        }
        SetupChessman();
    }
    private void SetupChessman()
    {
        int chessmanIndex = 0;
        for (int i = 0; i < settings.TeamSettings.Length; i++)
        {
            GameObject parentTile = settings.TeamSettings[i].TeamColor == TeamColor.white ? new GameObject("TeamWhite") : new GameObject("TeamBlack");
            parentTile.transform.SetParent(boardParent.transform);

            foreach (var teamChessman in settings.TeamSettings[i].ChessmanList)
            {
                foreach (var typePos in teamChessman.StartingPosition)
                {
                    GameObject prefab = teamChessman.Type.Name.ChessmanPrefab;
                    int posX = typePos.posX;
                    int posZ = typePos.posZ;
                    Tile tile = chessBoard[posX][posZ];
                    //Spróbowaæ to uproœciæ
                    GameObject chessmanClone = Instantiate(prefab, new Vector3(tile.transform.position.x, 0.5f, tile.transform.position.z), 
                        Quaternion.Euler(0, teamChessman.YrotationDegrees, 0));
                    chessmanClone.transform.SetParent(parentTile.transform);
                    
                    Chessman chessman = chessmanClone.GetComponent<Chessman>();
                    chessman.SetTeamColor(settings.TeamSettings[i].TeamColor);
                    chessman.UpdatePositionOnGrid(typePos.posX, typePos.posZ);
                    
                    tile.Chessman = chessman;
                    chessmanList.Add(chessman);
                    settings.TeamSettings[i].ChessmanDictionary.Add(tile.Chessman, chessmanIndex);
                    chessmanIndex++;
                }
            }
            chessmanIndex = 0;
        }
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
    //Przemyslec otrzymywanie indexu
    public int GetChessmanIndex(Chessman chessman, TeamColor activePlayer)
    {
        for (int i = 0; i < TeamSettings.Length; i++)
        {
            if (TeamSettings[i].TeamColor == activePlayer)
            {
                return settings.TeamSettings[i].ChessmanDictionary[chessman];
            }
        }
        return 0;
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
                    tile.ChangeColor(Color.red);
                }
                else if (tileWithChessmanBonusMove.HasBonusMoveableChessman())
                {
                    if(activePlayer != tileWithChessmanBonusMove.TeamColor)
                    {
                        tile.ChangeColor(Color.red);
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
            chessmanList.Remove(enemyChessman);
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
            chessmanList.Remove(enemyChessman);
            Destroy(enemyChessman.gameObject);
        }

        previousTile.Chessman = null;
        selectedTile.Chessman = selectedChessman;
    }
}