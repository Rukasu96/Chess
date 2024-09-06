interface ICastlingable
{
    bool CanUseCastling();
    void UpdateSatusCastling();
    Tile ReturnCastlingTile(Chessman selectedChessman);
}
