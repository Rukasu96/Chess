using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CheckTileStatusController checkStatusController;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private WinPanel winPanel;

    private IKingable whiteKing;
    private IKingable blackKing;

    public void SetKing(Chessman king)
    {
        if(king.GetTeamColor() == TeamColor.white)
        {
            whiteKing = king.GetComponent<IKingable>();
        }else
        {
            blackKing = king.GetComponent<IKingable>();
        }
    }

    public bool IsCheck()
    {
        List<Tile> whiteCheckTiles = checkStatusController.WhiteCheckTiles;
        List<Tile> blackCheckTiles = checkStatusController.BlackCheckTiles;

        if (whiteCheckTiles.Count > 0 || blackCheckTiles.Count > 0)
        {
            if (turnManager.ActivePlayer == TeamColor.white)
            {
                var tileWithCheckWhitKing = blackCheckTiles.Where(x => x.Chessman != null && x.Chessman.GetComponent<IKingable>() == whiteKing).FirstOrDefault(x => x.Chessman);

                if (tileWithCheckWhitKing != null)
                {
                    tileWithCheckWhitKing.GetComponent<Renderer>().material.color = Color.red;
                    return true;
                }
            }

            var tileWithCheckBlackKing = whiteCheckTiles.Where(x => x.Chessman != null && x.Chessman.GetComponent<IKingable>() == blackKing).FirstOrDefault(x => x.Chessman);

            if (tileWithCheckBlackKing != null)
            {
                tileWithCheckBlackKing.GetComponent<Renderer>().material.color = Color.red;
                return true;
            }
        }

        return false;
    }

    public void TryCheckMate()
    {
        List<Tile> whiteCheckTiles = checkStatusController.WhiteCheckTiles;
        List<Tile> blackCheckTiles = checkStatusController.BlackCheckTiles;

        if (whiteCheckTiles.Count > 0 || blackCheckTiles.Count > 0)
        {
            if (turnManager.ActivePlayer == TeamColor.white)
            {
                if (!blackKing.HasFreeTileToMove(whiteCheckTiles))
                {
                    CheckMate(TeamColor.white);
                }
            }
            else if (!whiteKing.HasFreeTileToMove(blackCheckTiles))
            {
                CheckMate(TeamColor.black);
            }
        }
    }
           

    private void CheckMate(TeamColor winTeam)
    {
        winPanel.SetWinnerTeamText(winTeam);
        winPanel.ActivePanel();
    }
}
