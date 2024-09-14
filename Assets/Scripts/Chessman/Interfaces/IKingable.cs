using System.Collections.Generic;

interface IKingable
{
    bool CanUseCastling();
    void UpdateCastlingStatus();
    bool HasFreeTileToMove(List<Tile> checkTiles);
}
