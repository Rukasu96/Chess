using System.Collections.Generic;

public class King : Chessman, IKingable
{
    private bool canUseCastling = true;

    public bool CanUseCastling()
    {
        return canUseCastling;
    }

    public bool HasFreeTileToMove(List<Tile> checkTiles)
    {
        List<Chessman> allyChessmans = new List<Chessman>();

        for (int i = 0; i < Setting.MovePatterns.Count; i++)
        {
            int posX = Setting.MovePatterns[i].posX;
            int posZ = Setting.MovePatterns[i].posZ;

            Tile tile = boardManager.ReturnTile(position.posX + posX, position.posZ + posZ);

            if (tile != null)
            {
                if (tile.Chessman != null && tile.Chessman.GetTeamColor() != teamColor)
                {
                    if (!checkTiles.Contains(tile))
                    {
                        return true;
                    }
                }
                else if(tile.Chessman == null)
                {
                    if (!checkTiles.Contains(tile))
                    {
                        return true;
                    }
                }
                else if(tile.Chessman.GetTeamColor() == teamColor)
                {
                    allyChessmans.Add(tile.Chessman);
                }
            }

            if(allyChessmans.Count == Setting.MovePatterns.Count - 3)
            {
                return true;
            }
        }

        return false;
    }

    public void UpdateCastlingStatus()
    {
        canUseCastling = false;
    }
}
