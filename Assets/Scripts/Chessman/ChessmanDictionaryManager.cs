using UnityEngine;

public class ChessmanDictionaryManager : MonoBehaviour
{
    [SerializeField] private TeamSettings[] teamSettings;

    public void AddChessman(TeamSettings setting, Chessman chessman, int chessmanIndex)
    {
        setting.ChessmanDictionary.Add(chessman, chessmanIndex);
    }

    public int ReturnChessmanIndex(Chessman chessman, TeamColor activePlayer)
    {
        for (int i = 0; i < teamSettings.Length; i++)
        {
            if (teamSettings[i].TeamColor == activePlayer)
            {
                return teamSettings[i].ChessmanDictionary[chessman];
            }
        }
        return 0;
    }
}
