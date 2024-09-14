using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ChessmanSpawner : MonoBehaviour
{
    private ChessmanDictionaryManager dictionaryManager;

    IChessmanFactory chessmanFactory = new ChessmanFactory();

    public void SpawnChessmans(BoardSetting setting, Tile[][] boardTiles)
    {
        dictionaryManager = GetComponent<ChessmanDictionaryManager>();

        int chessmanIndex = 0;
        for (int i = 0; i < setting.TeamSettings.Length; i++)
        {
            foreach (var teamChessman in setting.TeamSettings[i].ChessmanList)
            {
                foreach (var typePos in teamChessman.StartingPosition)
                {
                    int posX = typePos.posX;
                    int posZ = typePos.posZ;
                    Tile tile = boardTiles[posX][posZ];

                    Chessman chessman = chessmanFactory.Create(teamChessman.Type);
                    dictionaryManager.AddChessman(setting.TeamSettings[i], chessman, chessmanIndex);

                    chessman.SetTeamColor(setting.TeamSettings[i].TeamColor);
                    chessman.UpdatePositionOnGrid(typePos.posX, typePos.posZ);
                    chessman.SetChessmanOnTile(tile);

                    tile.Chessman = chessman;
                    chessmanIndex++;
                }
            }
            chessmanIndex = 0;
        }
    }
}
