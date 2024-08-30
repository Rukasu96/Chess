using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    [SerializeField] BoardSettings settings;
    private Tile[][] chessBoard;
    private GameObject boardParent;
    private List<Chessman> chessmanList = new List<Chessman>();
    public TeamSettings[] TeamSettings => settings.teamSettings;
    public GameObject BoardParent => boardParent;

    private void Awake()
    {
        SetupBoard();
    }
    private void SetupBoard()
    {
        boardParent = new GameObject("BoardParent");
        boardParent.transform.position = new Vector3(settings.BoardPositionX, 0f, settings.BoardPositionZ); 

        chessBoard = new Tile[settings.width][];

        for (int i = 0; i < settings.width; i++)
        {
            chessBoard[i] = new Tile[settings.height];
        }

        CreateTiles();
    }
    private void CreateTiles()
    {
        float tileBoardCenterOffsetX = (settings.width + settings.height) / (settings.width / 2) - settings.BoardPositionX;
        float tileBoardCenterOffsetZ = (settings.width + settings.height) / (settings.height / 2) - settings.BoardPositionZ;
        Color currentColor;
        bool oddRow = false;

        for (int i = 0; i < settings.width; i++)
        {
            currentColor = oddRow ? Color.white : Color.black;
            
            for (int j = 0; j < settings.height; j++)
            {
                Tile tileClone = Instantiate(settings.tilePrefab, new Vector3(i * settings.tileOffsetPosition - tileBoardCenterOffsetX, 0, j * settings.tileOffsetPosition - tileBoardCenterOffsetZ), Quaternion.identity);
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
        for (int i = 0; i < settings.teamSettings.Length; i++)
        {
            GameObject parentTile = settings.teamSettings[i].teamColor == TeamColor.white ? new GameObject("TeamWhite") : new GameObject("TeamBlack");
            parentTile.transform.SetParent(boardParent.transform);

            foreach (var teamChessman in settings.teamSettings[i].chessmanList)
            {
                foreach (var typePos in teamChessman.startingPosition)
                {
                    GameObject prefab = teamChessman.type.Name.chessmanPrefab;
                    int posX = typePos.posX;
                    int posZ = typePos.posZ;
                    Tile tile = chessBoard[posX][posZ];
                    //Spróbowaæ to uproœciæ
                    GameObject chessmanClone = Instantiate(prefab, new Vector3(tile.transform.position.x, 0.5f, tile.transform.position.z), 
                        Quaternion.Euler(0, teamChessman.YrotationDegrees, 0));
                    chessmanClone.transform.SetParent(parentTile.transform);
                    
                    Chessman chessman = chessmanClone.GetComponent<Chessman>();
                    chessman.SetTeamColor(settings.teamSettings[i].teamColor);
                    chessman.UpdatePositionOnGrid(typePos.posX, typePos.posZ);
                    
                    tile.Chessman = chessman;
                    chessmanList.Add(chessman);
                    settings.teamSettings[i].ChessmanDictionary.Add(tile.Chessman, chessmanIndex);
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
            if (TeamSettings[i].teamColor == activePlayer)
            {
                return settings.teamSettings[i].ChessmanDictionary[chessman];
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

                if (tile.Chessman != null)
                {
                    tile.ChangeColor(Color.red);
                }
                else if (tileWithChessmanBonusMove.HasBonusMoveableChessman())
                {
                    if(activePlayer != tileWithChessmanBonusMove.teamColor)
                    {
                        tile.ChangeColor(Color.red);
                    }
                }
            }
            else
                tile.BackToDefaultColor();

            Debug.Log(tile.Chessman);
        }
    }

    public void UpdateBoard(Tile selectedTile, Chessman selectedChessman)
    {
        Tile previousTile = ReturnTile(selectedChessman.GetPosition().posX, selectedChessman.GetPosition().posZ);
        Chessman enemyChessman = selectedTile.Chessman;

        Tile tileWithChessmanBonusMove;

        if (selectedChessman.GetTeamColor() == TeamColor.white)
        {
            tileWithChessmanBonusMove = ReturnTile(selectedTile.PositionOnGrid.posX + 1, selectedTile.PositionOnGrid.posZ);
        }
        else
        {
            tileWithChessmanBonusMove = ReturnTile(selectedTile.PositionOnGrid.posX - 1, selectedTile.PositionOnGrid.posZ);
        }

        if (enemyChessman != null)
        {
            selectedTile.Chessman = null;
            chessmanList.Remove(enemyChessman);
            Destroy(enemyChessman.gameObject);
        }
        else if (tileWithChessmanBonusMove.HasBonusMoveableChessman())
        {
            Tile tileWithChessman;
            tileWithChessmanBonusMove.RemovebonusMoveableChessman();

            if (selectedChessman.GetTeamColor() == TeamColor.white)
            {
                tileWithChessman = ReturnTile(selectedTile.PositionOnGrid.posX - 1, selectedTile.PositionOnGrid.posZ);
            }
            else
            {
                tileWithChessman = ReturnTile(selectedTile.PositionOnGrid.posX + 1, selectedTile.PositionOnGrid.posZ);
            }

            enemyChessman = tileWithChessman.Chessman;
            tileWithChessman.Chessman = null;
            chessmanList.Remove(enemyChessman);
            Destroy(enemyChessman.gameObject);
        }

        previousTile.Chessman = null;
        selectedTile.Chessman = selectedChessman;
    }
}
