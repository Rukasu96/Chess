using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICastlingable
{
    bool CanUseCastling();
    void UpdateSatusCastling();
    Tile ReturnCastlingTile(Chessman selectedChessman);
}
