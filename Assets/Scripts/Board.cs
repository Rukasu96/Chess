using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    [SerializeField] BoardSettings settings;
    private Tile[][] chessBoard;
    public TeamSettings[] TeamSettings => settings.teamSettings;

    private void Awake()
    {
        SetupBoard();
    }
    private void SetupBoard()
    {
        chessBoard = new Tile[settings.width][];

        for (int i = 0; i < settings.width; i++)
        {
            chessBoard[i] = new Tile[settings.height];
        }

        CreateTiles();
    }
    private void CreateTiles()
    {
        Color currentColor;
        bool oddRow = false;

        GameObject parentTile = new GameObject("ChessBoard");

        for (int i = 0; i < settings.width; i++)
        {
            currentColor = oddRow ? Color.white : Color.black;
            
            for (int j = 0; j < settings.height; j++)
            {
                Tile tileClone = Instantiate(settings.tilePrefab, new Vector3(i * settings.tileOffsetPosition, 0, j * settings.tileOffsetPosition), Quaternion.identity);
                tileClone.transform.SetParent(parentTile.transform);
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
                    chessman.PositionUpdate(typePos.posX, typePos.posZ);

                    tile.Chessman = chessman;
                    settings.teamSettings[i].ChessmanDictionary.Add(tile.Chessman, chessmanIndex);
                    chessmanIndex++;
                }
            }
            chessmanIndex = 0;
        }
    }
    public int GetChessmanIndex(Chessman chessman)
    {
        for (int i = 0; i < TeamSettings.Length; i++)
        {
            if (TeamSettings[i].teamColor == chessman.GetTeamColor())
            {
                return settings.teamSettings[i].ChessmanDictionary[chessman];
            }
        }
        return 0;
    }
    private Tile ReturnTile(int posX, int posZ)
    {
        return chessBoard[posX][posZ];
    }
    public void ChangeTileColor(Chessman selectedChessman, Color color)
    {
        var TilesPositionsToChange = selectedChessman.GetMovePatterns();
        var TilePositionWithChessman = selectedChessman.GetPosition();

        foreach (var movePattern in TilesPositionsToChange)
        {
            Tile tile = ReturnTile(TilePositionWithChessman.posX + movePattern.posX, TilePositionWithChessman.posZ + movePattern.posZ);
            tile.tag = tile.tag != "Selectable" ? "Selectable" : "Untagged";

            if (tile.tag == "Selectable")
                tile.ChangeColor(color);
            else
                tile.BackToDefaultColor();
        }
    }
    public void UpdateBoard(Tile selectedTile, Chessman selectedChessman)
    {
        Tile previousTile = ReturnTile(selectedChessman.GetPosition().posX, selectedChessman.GetPosition().posZ);
        previousTile.Chessman = null;
        selectedTile.Chessman = selectedChessman;
        Vector3 destinationTile = new Vector3(selectedTile.transform.position.x, 0.5f, selectedTile.transform.position.z);
        selectedChessman.PositionUpdate(selectedTile.PositionOnGrid.posX, selectedTile.PositionOnGrid.posZ);
        selectedChessman.MoveToNewTile(destinationTile);
    }
}
